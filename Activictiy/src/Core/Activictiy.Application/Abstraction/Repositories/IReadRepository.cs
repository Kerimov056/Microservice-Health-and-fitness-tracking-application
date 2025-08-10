using Activictiy.Domain.Entitys.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Activictiy.Application.Abstraction.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : BaseEntity, new()
{
    IQueryable<T> GetAll(bool isTracking = true, params string[] includes);
    IQueryable<T> GetAllExpression(Expression<Func<T, bool>> expression, int skip, int take, bool IsTracking = true, params string[] includes);
    IQueryable<T> GetAllExpressionOrderBy(Expression<Func<T, bool>> expression, int skip, int take, Expression<Func<T, object>> expressionOrder, bool IsOrder = true, bool isTracking = true, params string[] inculdes);
    Task<T> GetByIdAsync(Guid Id);
    Task<T> GetByIdAsyncExpression(Expression<Func<T, bool>> expression, bool IsTracking = true);
}
