using EasyPay.Common.Helper;
using EasyPay.Data.DatabaseContext;
using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;
using EasyPay.Services.Auth.Interface;

namespace EasyPay.Services.Auth.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork<EasyPayDbContext> _db;
        public AuthService(IUnitOfWork<EasyPayDbContext> db) 
        {
            _db = db;
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _db.UserRipository.GetAsync(p => p.UserName == username);
            if (user ==null)
                return null;
            
            if (!Utilities.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            
            Utilities.CreatePasswordHash(password, out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;

            await _db.UserRipository.AddAsync(user);
            await _db.SaveAsync();

            return user;

        }
    }
}
