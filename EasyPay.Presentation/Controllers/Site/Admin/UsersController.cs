using AutoMapper;
using EasyPay.Data.DatabaseContext;
using EasyPay.Data.Dto.Site.Admin.Users;
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
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork<EasyPayDbContext> unitOfWork , IMapper mapper)
        {
            _db = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _db.UserRipository.GetAllAsync();
            var UsersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(Users);
            return Ok(UsersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var User = await _db.UserRipository.GetByIdAsync(id);
            var UserToReturn = _mapper.Map<UserForDetailedDto>(User);
            return Ok(UserToReturn);
        }
    }
}
