using MongoDB.Driver;
using SGM.GEP.Domain.Entities;
using SGM.GEP.Domain.Interfaces.Repositories;
using SGM.GEP.Infra.Data.Mongo.Contexts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.GEP.Infra.Data.Mongo.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly GEPContextMongo _context;
        public GenericRepository(GEPContextMongo context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _context.GetCollection<TEntity>().InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        public IQueryable<TEntity> GetAll()
        {
           return _context.GetCollection<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            var queryResut = await _context.GetCollection<TEntity>().FindAsync(filter, cancellationToken: cancellationToken);

            return await queryResut.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var queryResut = await _context.GetCollection<TEntity>().FindAsync(f => f.Id == id, cancellationToken: cancellationToken);

            return await queryResut.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
