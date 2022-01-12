using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nasa.Infrastructure.Persistence;

namespace Nasa.Infrastructure;

public class AutoMigration
{
    public static async Task MigrateAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<DatabaseContext>();

        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync();
        }
    }
}