using AutoMapper;
using MemberManagementAPI.Data;
using MemberManagementAPI.Interfaces;
using MemberManagementAPI.Models;
using MemberManagementAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;

namespace MemberManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IMemberRepository _memberRepository;

        public OrganizationController(IMapper mapper, IOrganizationRepository organizationRepository, IMemberRepository memberRepository) {
            _mapper = mapper;
            _organizationRepository = organizationRepository;
            _memberRepository = memberRepository;
        }
        [HttpGet]
        public IActionResult GetAll() {
            var OrgList = _mapper.Map<List<OrganizationModel>>(_organizationRepository.GetAllOrganizations().ToList());
            return Ok(OrgList);

        }

        [HttpGet("{type:alpha}")]
        public IActionResult GetOrganizationByType(string type)
        {
            if (!ModelState.IsValid)
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
            if (OrganizationCreate == null) { return BadRequest(ModelState); }

            var organization = _organizationRepository.GetAllOrganizations()
                .Where(o => o.Name.Trim().ToUpper() == OrganizationCreate.Name.Trim().ToUpper() && o.Type == OrganizationCreate.Type)
                .FirstOrDefault();

            if (organization != null) {
                ModelState.AddModelError("error", "Tên hội này đã tồn tại");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var organizationMap = _mapper.Map<Organization>(OrganizationCreate);

            _organizationRepository.CreateOrganization(organizationMap);
            

            var organizationCreated = _mapper.Map<OrganizationModel>(organizationMap);
            return CreatedAtAction(nameof(GetOrganizationById), new { orgId = organizationCreated.OrgID }, organizationCreated);

        }

        [HttpDelete("{orgId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult DeleteOrganization(Guid orgId) {
            if (!_organizationRepository.OrganizationExists(orgId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var organizationToDelete = _organizationRepository.GetOrganization(orgId);
            if (!_organizationRepository.DeleteOrganizationAndChild(organizationToDelete))
            {
                ModelState.AddModelError("error", "Có lỗi xảy ra khi xóa tổ chức!");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpPost("manager/orgId={orgId}&memberId={memberId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]

        public IActionResult CreateManager(Guid memberId, Guid orgId )
        {
            if(!_organizationRepository.OrganizationExists(orgId) || !_memberRepository.MemberExists(memberId) )
            {
                return NotFound();
            }    

            if(_memberRepository.GetMember(memberId).CurrentOrganizationID != orgId) {
                ModelState.AddModelError("error", "Người này không thuộc hội");
                return StatusCode(422, ModelState);
            }
            if(_organizationRepository.GetAllManager(orgId).Any(m => m.MemberID == memberId)) {

                ModelState.AddModelError("error", "Người này đã làm quản lý của hội");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_organizationRepository.CreateManager(memberId, orgId))
            {
                ModelState.AddModelError("error", "Có lỗi xảy ra khi tạo quản lý!");
                return StatusCode(500, ModelState);
            }
            return Ok(_mapper.Map<MemberModel>(_memberRepository.GetMember(memberId)));
        }

        [HttpGet("manager/{orgId}")]
        public IActionResult GetAllManagerById(Guid orgId)
        {
            if (!_organizationRepository.OrganizationExists(orgId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managerList = _organizationRepository.GetAllManager(orgId).ToList();
            var map = _mapper.Map<List<MemberModel>>(managerList);
            return Ok(map);
        }

        [HttpDelete("manager/{memberId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]

        public IActionResult DeleteManager(Guid memberId)
        {
            if(!_memberRepository.MemberExists(memberId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_organizationRepository.DeleteManager(memberId))
            {
                ModelState.AddModelError("error", "Có lỗi xảy ra khi xóa quản lý!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
