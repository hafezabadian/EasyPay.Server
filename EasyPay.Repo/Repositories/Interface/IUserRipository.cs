using EasyPay.Data.Model;
using EasyPay.Repo.Infrastructure;

namespace EasyPay.Repo.Repositories.Interface
{
    public interface IUserRipository : IRepository<User>
    {
        Task<bool> UserExists(string username);
    }
}
