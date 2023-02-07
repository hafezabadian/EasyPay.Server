using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;
using EasyPay.Repo.Repositories.Interface;
using EasyPay.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Repo.Repositories.Repositoriy
{
    internal class UserRepository : Repository<User>, IUserRipository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext db) : base(db)
        {
            _db = _db ?? (EasyPayDbContext) db;
        }
    }
}
