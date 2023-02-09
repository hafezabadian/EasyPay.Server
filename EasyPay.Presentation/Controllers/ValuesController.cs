using EasyPay.Data.DatabaseContext;
using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyPay.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUnitOfWork<EasyPayDbContext> _db;
        public ValuesController(IUnitOfWork<EasyPayDbContext> unitOfWork)
        {
            _db= unitOfWork;
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var user = new User()
            {
                Address = "",
                City = "",
                DateOfBirth = DateTime.Now,
                Gender = true,
                IsAcive = true,
                Name = "",

                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, },

                PhoneNumber = "",
                Status = true,
                UserName = ""
            };

            await _db.UserRipository.AddAsync(user);
            await _db.SaveAsync();

            var model = await _db.UserRipository.GetAllAsync();

            return Ok(model);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
