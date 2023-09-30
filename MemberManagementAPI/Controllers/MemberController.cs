using MemberManagementAPI.Data;
using MemberManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemberManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        public readonly MyDBContext _context;
        private readonly AppSettings _appSettings;

        public MemberController(MyDBContext context, AppSettings appSettings)
        {
            _context = context;
            _appSettings = appSettings;
        }

        

    }
}
