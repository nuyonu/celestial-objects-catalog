using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nasa.Application.Common.Interfaces;
using Nasa.Infrastructure.Persistence;
using Nasa.Infrastructure.Repositories;

namespace Nasa.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.RegisterDatabase();
        
        services.RegisterRepositories();
        
        return services;
    }

    private static void RegisterDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=NasaCatalog;Integrated Security=True",
                opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
    }
    
    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
    }
}