using AutoMapper;
using MemberManagementAPI.Data;
using MemberManagementAPI.Interfaces;
using MemberManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemberManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;

        public MemberController(IMapper mapper, IMemberRepository memberRepository)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var memberList = _mapper.Map<List<MemberModel>>(_memberRepository.GetAllMembers().ToList());
            return Ok(memberList);

        }
        [HttpGet("{memberId}")]
        public IActionResult GetMemberById(Guid memberId) {
            if (_memberRepository.MemberExists(memberId))
            {
                return NotFound();
            }
            var member = _memberRepository.GetMember(memberId);
            var memberMap = _mapper.Map<MemberModel>(member);
            return Ok(memberMap);
        }
        [HttpGet("{memberId}/Organization")]
        public IActionResult GetOrganizationOfMember(Guid memberId) {
            if (!_memberRepository.MemberExists(memberId)) { return NotFound(); }

            var organization = _memberRepository.GetOrganizationOfMember(memberId);

            if (organization == null) { return NotFound(); }
            var organizationMap = _mapper.Map<OrganizationModel>(organization);

            if (!ModelState.IsValid) { return BadRequest(); }
            return Ok(organizationMap);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult CreateMember([FromBody] MemberModel memberCreate)
        {
            if (memberCreate == null)
            {
                return BadRequest(ModelState);
            }

            var member = _memberRepository.GetAllMembers().Where(m => m.PhoneNumber == memberCreate.PhoneNumber).FirstOrDefault();
            if (member != null && (member.PhoneNumber != null && member.PhoneNumber != ""))
            {
                ModelState.AddModelError("error", "Đã có thành viên sử dụng SĐT này");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            memberCreate.JoinDate = DateTime.Now;
            var memberMap = _mapper.Map<Member>(memberCreate);
            if (!_memberRepository.CreateMember(memberMap))
            {
                ModelState.AddModelError("", "Có lỗi đã xảy ra khi tạo thành viên!");
                return StatusCode(500, ModelState);
            }
            return CreatedAtAction(nameof(GetMemberById), new { memberId = memberMap.MemberID }, _mapper.Map<MemberModel>(memberMap));
        }

        [HttpDelete("{memberId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult DeleteMember(Guid memberId)
        {
            if (!_memberRepository.MemberExists(memberId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var memberDelete = _memberRepository.GetMember(memberId);
            if (!_memberRepository.DeleteMember(memberDelete))
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa tổ chức!");
            }
            return NoContent();
        }

        [HttpPut("{memberId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateMember(Guid memberId,[FromBody] MemberModel memberUpdate)
        {
            if(memberUpdate == null) { return BadRequest(ModelState); }

            if(memberId != memberUpdate.MemberID) { return BadRequest(ModelState); }

            if (!_memberRepository.MemberExists(memberId))
            {
                return NotFound();
            }
           /* var member = _memberRepository.GetAllMembers().Where(m => m.PhoneNumber == memberUpdate.PhoneNumber).FirstOrDefault();
            if (member != null && (member.PhoneNumber != null && member.PhoneNumber != ""))
            {
                ModelState.AddModelError("error", "Đã có thành viên sử dụng SĐT này");
                return StatusCode(422, ModelState);
            }*/

            if (!ModelState.IsValid) { return BadRequest(); }


            var memberMap = _mapper.Map<Member>(memberUpdate);

            if(!_memberRepository.UpdateMember(memberMap))
            {
                ModelState.AddModelError("error", "Có lỗi xảy ra khi cập nhật thành viên");
            }
            var memberUpdated = _mapper.Map<MemberModel>(memberMap);
            return Ok(memberUpdated);

        }



    }
}
