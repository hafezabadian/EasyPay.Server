using EasyPay.Data.Model;

namespace EasyPay.Services.Auth.Interface
{
    public interface IAuthService
    {
        Task<User> Register(User user,string password);
        Task<User> Login(string username, string password);
    }
}
