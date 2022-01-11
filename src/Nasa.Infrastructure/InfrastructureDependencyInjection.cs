using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nasa.Application.Common.Interfaces;
using Nasa.Infrastructure.Persistence;
using Nasa.Infrastructure.Repositories;

namespace Nasa.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterDatabase(configuration);

        services.RegisterRepositories();

        return services;
    }

    private static void RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        if (!bool.TryParse(configuration["Database:UseInMemory"], out var useInMemory))
        {
            useInMemory = true;
        }

        if (useInMemory)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("NasaDatabase");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
        else
        {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(configuration["Database:ConnectionString"],
                    opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
        }
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
    }
}