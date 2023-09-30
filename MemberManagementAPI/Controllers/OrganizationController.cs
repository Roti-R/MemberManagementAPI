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
            var OrgList = _mapper.Map<List<OrganizationModel>>(_organizationRepository.GetOrganizations().ToList());
            return Ok(OrgList);
            
        }

        [HttpGet("{type}")]
        public IActionResult GetOrganizationByType(string type)
        {
            var OrgList = _mapper.Map<List<OrganizationModel>>(_organizationRepository.GetOrganizationByType(type).ToList());
            return Ok(OrgList);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrganization(OrganizationModel OrganizationCreate)
        {
            if(OrganizationCreate == null) { return BadRequest(ModelState); }

            var organization = _organizationRepository.GetOrganizations()
                .Where(o => o.Name.Trim().ToUpper() == OrganizationCreate.Name.Trim().ToUpper() && o.Type == OrganizationCreate.Type)
                .FirstOrDefault();

            if(organization != null) { 
                ModelState.AddModelError("errorMessage", "Tên hội này đã tồn tại");
                return StatusCode(422, ModelState);                        
            }
            if(!ModelState.IsValid) { return BadRequest(ModelState); }

            var organizationMap = _mapper.Map<Organization>(OrganizationCreate);

            if(!_organizationRepository.CreateOrganization(organizationMap))
            {
                ModelState.AddModelError("", "Có lỗi đã xảy ra khi tạo!");
                return StatusCode(500, ModelState);
            }

            return Ok("Đã thêm thành công");
        }

        [HttpDelete("orgId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult DeleteOrganization(Guid orgId) {
            
        }
    }
}
