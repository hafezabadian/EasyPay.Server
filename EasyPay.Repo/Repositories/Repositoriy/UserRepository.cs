using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;
using EasyPay.Repo.Repositories.Interface;
using EasyPay.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace EasyPay.Repo.Repositories.Repositoriy
{
    internal class UserRepository : Repository<User>, IUserRipository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext db) : base(db)
        {
            _db = _db ?? (EasyPayDbContext)_db;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await GetAsync(p => p.UserName == username) == null)
                return false;
            return true;
        }
    }
}
