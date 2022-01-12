using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Nasa.API;
using Nasa.API.EndpointDefinitions;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application;
using Nasa.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinitionsMarker));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.RegisterApplication()
    .RegisterInfrastructure(builder.Configuration);

await AutoMigration.MigrateAsync(builder.Services.BuildServiceProvider());

var app = builder.Build();

app.UseHttpsRedirection();

app.UseEndpointDefinitions();

app.ConfigureExceptionHandler();

await app.RunAsync();

#pragma warning disable CA1050
namespace Nasa.API
{
    public partial class Program // For testing purpose
    { }
}
#pragma warning restore CA1050