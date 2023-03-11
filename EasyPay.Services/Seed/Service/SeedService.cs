using EasyPay.Common.Helper;
using EasyPay.Data.DatabaseContext;
using EasyPay.Data.Dto.Site.Admin;
using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;
using EasyPay.Services.Seed.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasyPay.Services.Seed.Service
{
    public class SeedService : ISeedService
    {   
        private readonly IUnitOfWork<EasyPayDbContext> _db;
        public SeedService(IUnitOfWork<EasyPayDbContext> db)
        {
            _db = db;
        }
        public void SeedUser()
        {
            var UserData = System.IO.File.ReadAllText("C:\\Users\\ali\\source\\repos\\EasyPay\\EasyPay.Presentation\\Files\\Json\\Seed\\UserSeedData.json");
            var Users = JsonConvert.DeserializeObject<IList<User>>(UserData);
            foreach (var user in Users)
            {
                user.UserName = user.UserName.ToLower();
                if (!_db.UserRipository.UserExists(user.UserName)) 
                {
                    byte[] PasswordHash, PasswordSalt;

                    Utilities.CreatePasswordHash("123456", out PasswordHash, out PasswordSalt);
                    user.PasswordHash = PasswordHash;
                    user.PasswordSalt = PasswordSalt;

                    _db.UserRipository.Add(user);
                }
            }
            _db.Save();
        }
    }
}
