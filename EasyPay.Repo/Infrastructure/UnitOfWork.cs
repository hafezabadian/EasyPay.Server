using EasyPay.Repo.Repositories.Interface;
using EasyPay.Repo.Repositories.Repositoriy;
using Microsoft.EntityFrameworkCore;


namespace EasyPay.Repo.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext , new()
    {
        #region ctor
        private readonly DbContext _db;
        public UnitOfWork()
        {
            _db = new TContext();
        }
        #endregion

        #region private repositories
        private IUserRipository userRipository;
        public IUserRipository UserRipository 
        { 
            get 
            {
                if(userRipository == null)
                {
                    userRipository = new UserRepository(_db);
                }
                return userRipository;
            } 
        }


        #endregion

        #region save
        public void Save()
        {
            _db.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _db.SaveChangesAsync();
        }
        #endregion

        #region Dispose
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if(disposing)
                {
                    _db.Dispose();
                }
            }
            disposed= true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }
        #endregion
    }
}
