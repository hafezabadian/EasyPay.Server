using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EasyPay.Repo.Infrastructure
{
    internal abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        #region ctor
        private readonly DbContext _db;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DbContext db)
        {
            _db = db;
            _dbSet= db.Set<TEntity>();
        }
        #endregion

        #region normal
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException("there is no entity");
            _dbSet.Update(entity);
        }
        public void Delete(object id)
        {
            TEntity entity = GetById(id);
            if (entity == null) 
                throw new ArgumentException("there is no entity");
            _dbSet.Remove(entity);
        }
        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        public void Delete(Expression<Func<TEntity, bool>> exception)
        {
            IEnumerable<TEntity> obj = _dbSet.Where(exception).AsEnumerable();
            foreach (TEntity entity in obj)
            {
                _dbSet.Remove(entity);
            }
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }
        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }
        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }
        
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).AsEnumerable();
        }
        #endregion



        #region Async
        public async Task AddAsync(TEntity entity)
        {
           await _dbSet.AddAsync(entity);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).FirstOrDefaultAsync();
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }
        #endregion

        #region Dispose
        private bool disposed = false;
        public virtual void Dispose(bool desposing)
        {
            if (!disposed)
            {
                if (desposing)
                {
                    _db.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository() { Dispose(false); }
        #endregion

    }
}
