using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IQueryService<TEntity>
    {
        Task Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity retailer);
    }

    public interface IQueryService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        IQueryable<TEntity> Get();
       // Task<IEnumerable<TEntity>> Get(string query, string orderby, int pageNumber = 1, int pageSize = 25);
        Task<TEntity> GetById(object id);
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
    }
}