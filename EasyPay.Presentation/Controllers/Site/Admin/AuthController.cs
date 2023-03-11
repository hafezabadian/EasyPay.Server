using EasyPay.Common.ErrorAndMessage;
using EasyPay.Data.DatabaseContext;
using EasyPay.Data.Dto.Site.Admin;
using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;
using EasyPay.Services.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyPay.Presentation.Controllers.Site.Admin
{
    [Route("site/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork<EasyPayDbContext> _db;
        private readonly IAuthService _authService;
        private readonly IConfiguration _Configuration;
        public AuthController(IUnitOfWork<EasyPayDbContext> unitOfWork, IAuthService authService,IConfiguration Configuration)
        {
            _db = unitOfWork;
            _authService = authService;
            _Configuration = Configuration;
        }


        // Register: site/admin/<ValuesController>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _db.UserRipository.UserExistsAsync(userForRegisterDto.UserName))
            {
                return BadRequest(
                    new ReturnMessage {
                        code = "",
                        title = "خطا",
                        message = "این نام کاربری وجود دارد",
                        
                    });
            }

            var tobecreateduser = new User()
            {
                UserName = userForRegisterDto.UserName,
                Name = userForRegisterDto.Name,
                PhoneNumber= userForRegisterDto.PhoneNumber,
                Address = "",
                City = "",
                DateOfBirth = DateTime.Now,
                Gender = true,
                IsAcive = true,
                Status = true,
            };

            var user = await _authService.Register(tobecreateduser, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [AllowAnonymous]
        // Login: site/admin/<ValuesController>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto useForLoginDto)
        {
            var userFromRepo = await _authService.Login(useForLoginDto.UserName, useForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized("کاربری با این یوزر و پس وجود ندارد");

                //return Unauthorized(new ReturnMessage()
                //{
                //    status = false,
                //    title = "خطا",
                //    message = "کاربری با این یوزر و پس وجود ندارد"
                //});

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = useForLoginDto.IsRemember ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(2),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDes);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }





        [AllowAnonymous]
        [HttpGet("GetValue")]
        public async Task<IActionResult> GetValue()
        {
            return Ok(new ReturnMessage()
            {
                status = true,
                title = "اوکی",
                message = ""
            });
        }

        [HttpGet("GetValues")]
        public async Task<IActionResult> GetValues()
        {
            return Ok(new ReturnMessage()
            {
                status = true,
                title = "اوکی",
                message = ""
            });
        }
    }
}
