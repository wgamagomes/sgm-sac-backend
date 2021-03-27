using SGM.SAC.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.SAC.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : Entity
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
    }
}
