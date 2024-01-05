using MemberManagementAPI.Data;
using MemberManagementAPI.Models;
using static MemberManagementAPI.Utilities.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MemberManagementAPI.Utilities;

namespace MemberManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly MyDBContext _context;
        private readonly AppSettings _appSettings;

        public AccountController(MyDBContext context, IOptions<AppSettings> appsetting)
        {
            _context = context;
            _appSettings = appsetting.Value;
        }


        [HttpPost]
        [Route("create")]
        public IActionResult Create(AccountModel user, Guid AccountID)
        {
            try
            {
                var tempUser = new Account
                {
                    MemberID = AccountID, 
                    Username = user.Username,
                    Password = Hash256Password(user.Password),
                };
                _context.Accounts.Add(tempUser);
                _context.SaveChanges();
                return Ok(new
                {
                    Success = true,
                    Data = tempUser
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("login")]
        
        public IActionResult login(string username, string password)
        {

            try
            {
                var user = _context.Accounts.SingleOrDefault(s => s.Username == username && s.Password == Hash256Password(password));
                if(user == null)
                {
                    return Ok( new
                    {
                        Success = false,
                        Message = "Tài khoản và mật khẩu không đúng"
                    }
                    );
                }

                //Cấp token nếu user và password đúng
                return Ok(new
                {
                    Success = true,
                    Message = "Đăng nhập thành công",
                    Data = GenerateToken(username, password)
                }) ;
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateToken(string username, string password)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var bytedSecretKey = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("username", username),
                } ),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(bytedSecretKey), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
