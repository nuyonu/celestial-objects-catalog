using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Nasa.Infrastructure.Persistence;

namespace Nasa.API.IntegrationTests;

public class NasaAppFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DatabaseContext>));
            services.AddDbContext<DatabaseContext>(options =>
                options.UseInMemoryDatabase("NasaTesting"));
        });
    }
}