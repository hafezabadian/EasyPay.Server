using EasyPay.Data.DatabaseContext;
using EasyPay.Repo.Infrastructure;
using EasyPay.Services.Auth.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyPay.Presentation.Controllers.Site.Admin
{
    [Route("site/admin/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork<EasyPayDbContext> _db;
        public UsersController(IUnitOfWork<EasyPayDbContext> unitOfWork)
        {
            _db = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _db.UserRipository.GetAllAsync();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var User = await _db.UserRipository.GetByIdAsync(id);
            return Ok(User);
        }
    }
}
