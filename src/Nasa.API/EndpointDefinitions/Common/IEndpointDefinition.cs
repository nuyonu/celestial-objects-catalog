namespace Nasa.API.EndpointDefinitions.Common;

public interface IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app);
    
    public void DefineServices(IServiceCollection services);
}