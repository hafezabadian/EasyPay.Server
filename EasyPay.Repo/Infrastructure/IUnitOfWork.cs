using Microsoft.EntityFrameworkCore;


namespace EasyPay.Repo.Infrastructure
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        void Save();
        Task<int> SaveAsync();
    }
}
