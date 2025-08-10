using Activictiy.Domain.Entitys;
using Microsoft.EntityFrameworkCore;

namespace Activictiy.Persistance.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

    public DbSet<Activity> Activities { get; set; }
   
}
