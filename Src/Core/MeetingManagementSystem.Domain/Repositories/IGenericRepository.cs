using MeetingManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace MeetingManagementSystem.Domain.Repositories
{
    public interface IGenericRepository<TEntity>  where TEntity : BaseEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetByIdAsync(Guid id);

        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(Guid id);
        Task RemoveRangeAsync(List<Guid> ids);
    }
}
