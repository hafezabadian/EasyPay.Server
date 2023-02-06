using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.Identity.Client;

namespace EasyPay.Data.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        #region CRUD
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> exception);
        #endregion

        #region FUNC
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        TEntity Get(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region CRUD Async
        Task AddAsync(TEntity entity);
        #endregion

        #region FUNC Async
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> expression);
        #endregion
    }

}
