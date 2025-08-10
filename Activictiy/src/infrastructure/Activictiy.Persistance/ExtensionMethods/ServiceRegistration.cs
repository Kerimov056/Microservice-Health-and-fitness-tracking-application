using Activictiy.Application.Abstraction.Repositories.IEntityRepsitory;
using Activictiy.Application.Abstraction.Services;
using Activictiy.Persistance.Context;
using Activictiy.Persistance.Implementations.Repositories.EntityRepository;
using Activictiy.Persistance.Implementations.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Activictiy.Persistance.ExtensionMethods;

public static class ServiceRegistration
{
    public static void AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        //Repository
        services.AddScoped<IWriteActivityRepository, WriteActivityRepository>();
        services.AddScoped<IReadActivityRepository, ReadActivityRepository>();

        //Services
        services.AddScoped<IActivityService, ActivityService>();


        //Mapper
        //services.AddAutoMapper(typeof(BlogProfile).Assembly);

    }
}
