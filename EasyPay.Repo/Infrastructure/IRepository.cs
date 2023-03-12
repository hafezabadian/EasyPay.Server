
using System.Linq.Expressions;


namespace EasyPay.Repo.Infrastructure
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
        /// <summary>
        /// this function return a list of user
        /// </summary>
        /// <param name="filter">null | get all query member or an exxpresion EX:((f => f.id == id), , )</param>
        /// <param name="orderby">null | use for sorting the query EX:( ,o => o.OrderByDescending(p  => p.id), )</param>
        /// <param name="IncludeEntity">null | string contine subproperty that want to include in user EX:(,,"Photo,BankCard")</param>
        /// <returns>a list of users</returns>
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string IncludeEntity = "");
        #endregion

        #region CRUD Async
        Task AddAsync(TEntity entity);
        #endregion

        #region FUNC Async
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        /// <summary>
        /// this async function return a list of users
        /// </summary>
        /// <param name="filter">can be null or get all query member or an exception [f => f.id == id]</param>
        /// <param name="orderby">can be null or use for sorting the query [ mt => mt.OrderByDescending(m => m.Name) ]</param>
        /// <param name="IncludeEntity">can be null or string contine subproperty that want to include in user ["Photo,BankCard"]</param>
        /// <returns>list of users</returns>
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>>? filter ,Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>>? orderby , string? IncludeEntity);
        #endregion
    }

}
