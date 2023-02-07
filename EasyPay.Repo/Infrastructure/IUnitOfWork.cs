using EasyPay.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;


namespace EasyPay.Repo.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        IUserRipository UserRipository { get; }
        void Save();
        Task<int> SaveAsync();
    }
}
