using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NeoServer.Data.Interfaces;

public interface IBaseRepositoryNeo<TEntity> where TEntity : class
{
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
    Task<IList<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(int id);
    Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
}