using Activictiy.Domain.Entitys.Common;

namespace Activictiy.Application.Abstraction.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entites);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entites);
    void Update(T entity);
    Task SaveChangeAsync();
}
