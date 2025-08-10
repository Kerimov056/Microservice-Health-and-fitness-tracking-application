using Activictiy.Application.Abstraction.Repositories;
using Activictiy.Domain.Entitys.Common;
using Activictiy.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Activictiy.Persistance.Implementations.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _appDbContext;
    public WriteRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;
    public DbSet<T> Table => _appDbContext.Set<T>();

    public async Task AddAsync(T entity) => await Table.AddAsync(entity);

    public async Task AddRangeAsync(IEnumerable<T> entites) => await Table.AddRangeAsync(entites);

    public void Remove(T entity) => Table.Remove(entity);

    public void RemoveRange(IEnumerable<T> entites) => Table.RemoveRange(entites);

    public void Update(T entity) => Table.Update(entity);

    public async Task SaveChangeAsync() => await _appDbContext.SaveChangesAsync();
}
