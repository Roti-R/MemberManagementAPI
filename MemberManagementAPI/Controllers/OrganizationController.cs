using AutoMapper;
using MemberManagementAPI.Data;
using MemberManagementAPI.Interfaces;
using MemberManagementAPI.Models;
using MemberManagementAPI.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MemberManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationRepository _organizationRepository;
        public OrganizationController(IMapper mapper, IOrganizationRepository organizationRepository) {
            _mapper = mapper;
            _organizationRepository = organizationRepository;
        }
        [HttpGet]
        public IActionResult GetAll() {
            var OrgList = _mapper.Map<List<OrganizationModel>>(_organizationRepository.GetAllOrganizations().ToList());
            return Ok(OrgList);
            
        }

        [HttpGet("{type:alpha}")]
        public IActionResult GetOrganizationByType(string type)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var OrgList = _mapper.Map<List<OrganizationModel>>(_organizationRepository.GetOrganization(type).ToList());
            return Ok(OrgList);

        }
        [HttpGet("{orgId}")]
        public IActionResult GetOrganizationById(Guid orgId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var organization = _mapper.Map<OrganizationModel>(_organizationRepository.GetOrganization(orgId));
            return Ok(organization);

        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult CreateOrganization([FromBody] OrganizationModel OrganizationCreate)
        {
            if(OrganizationCreate == null) { return BadRequest(ModelState); }

            var organization = _organizationRepository.GetAllOrganizations()
                .Where(o => o.Name.Trim().ToUpper() == OrganizationCreate.Name.Trim().ToUpper() && o.Type == OrganizationCreate.Type)
                .FirstOrDefault();

            if(organization != null) { 
                ModelState.AddModelError("errorMessage", "Tên hội này đã tồn tại");
                return StatusCode(422, ModelState);                        
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var organizationMap = _mapper.Map<Organization>(OrganizationCreate);

            if(!_organizationRepository.CreateOrganization(organizationMap))
            {
                ModelState.AddModelError("", "Có lỗi đã xảy ra khi tạo!");
                return StatusCode(500, ModelState);
            }

            var organizationCreated =  _mapper.Map<OrganizationModel>(organizationMap);
            return CreatedAtAction(nameof(GetOrganizationById), new { orgId = organizationCreated.OrgID }, organizationCreated);

        }

        [HttpDelete("{orgId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult DeleteOrganization(Guid orgId) {
            if(!_organizationRepository.OrganizationExists(orgId))
            {
                return NotFound();
            }

            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var organizationToDelete = _organizationRepository.GetOrganization(orgId);
            if(!_organizationRepository.DeleteOrganizationAndChild(organizationToDelete))
            {
                ModelState.AddModelError("", "Something wrong when delete organization");
            }
            return NoContent();

        }
    }
}
