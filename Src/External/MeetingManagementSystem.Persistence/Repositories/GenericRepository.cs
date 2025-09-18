using MeetingManagementSystem.Domain.Entities;
using MeetingManagementSystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeetingManagementSystem.Persistence.Repositories
{
    public class GenericRepository<TEntity , TContext> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entity;

        public GenericRepository(TContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if(entity!= null)
            {
                entity.IsActive = false;
                entity.UpdatedAt = DateTime.UtcNow;
                _entity.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRangeAsync(List<Guid> ids)
        {
            var entities = await _entity.Where(e => ids.Contains(e.Id)).ToListAsync();
            if (entities.Any())
            {
                foreach(var entity in entities)
                {
                    entity.IsActive = false;
                    entity.UpdatedAt = DateTime.UtcNow;
                }
                _entity.UpdateRange(entities);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _entity.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entity.Where(e=>e.IsActive).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.Where(e=>e.IsActive).FirstOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entity.Where(e=>e.IsActive).Where(filter).ToListAsync();
        }

    }
}
