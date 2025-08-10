using Activictiy.Domain.Entitys;
using Activictiy.Domain.Entitys.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Activictiy.Persistance.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Blogİmages> Blogİmages { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarImage> CarImages { get; set; }
    public DbSet<CarComment> CarComments { get; set; }
    public DbSet<CarCatogory> CarCatogories { get; set; }
}
