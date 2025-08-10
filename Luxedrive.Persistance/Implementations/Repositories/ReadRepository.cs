using Activictiy.Application.Abstraction.Repositories;
using Activictiy.Domain.Entitys.Common;
using Activictiy.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Activictiy.Persistance.Implementations.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _appDbContext;
    public ReadRepository(AppDbContext appDbContext) => _appDbContext = appDbContext;
    
    public DbSet<T> Table => _appDbContext.Set<T>();

    public IQueryable<T> GetAll(bool isTracking = true, params string[] includes)
    {
        var query = Table.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return isTracking ? query: query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int skip, int take, bool IsTracking = true, params string[] includes)
    {
        var query = Table.Where(expression).Skip(skip).Take(take).AsQueryable();
        foreach(var include in includes)
        {
            query = query.Include(include);
        }
        return IsTracking ? query : query.AsNoTracking();
    }

    public IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int skip, int take, Expression<Func<T, object>> expressionOrder, bool IsOrder = true, bool isTracking = true, params string[] inculdes)
    {
        var query = Table.Where(expression).AsQueryable();
        query = IsOrder ? query.OrderBy(expressionOrder) : query.OrderByDescending(expressionOrder);
        foreach(var include in inculdes)
        {
            query = query.Include(include);
        }
        return isTracking ? query : query.AsNoTracking();
    }

    public async Task<T> GetByIdAsync(Guid Id)
    {
        var query = await Table.FindAsync(Id);
        return query;
    }

    public async Task<T> GetByIdAsyncExpression(Expression<Func<T, bool>> expression, bool IsTracking = true)
    {
        var query = IsTracking ?  Table.AsQueryable() : Table.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }
}
