using Activictiy.Domain.Entitys.Common;
using Microsoft.EntityFrameworkCore;

namespace Activictiy.Application.Abstraction.Repositories;

public interface IRepository<T> where T : BaseEntity, new()
{
    public DbSet<T> Table { get; }
}
