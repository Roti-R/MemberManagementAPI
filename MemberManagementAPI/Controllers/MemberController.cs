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
        


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult CreateMember([FromBody] MemberModel memberCreate)
        {
            if(memberCreate == null)
            {
                return BadRequest(ModelState);
            }

            var member = _memberRepository.GetAllMembers().Where(m => m.PhoneNumber == memberCreate.PhoneNumber).FirstOrDefault();
            if(member != null && member.PhoneNumber != null)
            {
                ModelState.AddModelError("errorMessage", "Đã có thành viên sử dụng SĐT này");
                return StatusCode(422, ModelState);
            }

            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            memberCreate.JoinDate = DateTime.Now;
            var memberMap = _mapper.Map<Member>(memberCreate);
            if(!_memberRepository.CreateMember(memberMap))
            {
                ModelState.AddModelError("", "Có lỗi đã xảy ra khi tạo thành viên!");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }




    }
}
