using Nasa.API.EndpointDefinitions.Common;

namespace Nasa.API.EndpointDefinitions;

public class SwaggerDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}