using System;
using System.Threading.Tasks;
using Alba;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nasa.Infrastructure.Persistence;

namespace Nasa.API.IntegrationTests;

public class TestBase
{
    public static async Task<IAlbaHost> CreateAlbaHostAsync(string databaseName)
    {
        var albaHost = await AlbaHost.For<Program>(hostBuilder =>
        {
            hostBuilder.ConfigureServices((_, services) =>
            {
                services.AddMvcCore();
                
                services.RemoveAll(typeof(DbContextOptions<DatabaseContext>));
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseInMemoryDatabase(databaseName));
            });
        }, Array.Empty<IAlbaExtension>());

        return albaHost;
    }

    protected static T GetRequiredService<T>(IAlbaHost host) where T : notnull
    {
        var scope = host.Services.CreateScope();
        
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}