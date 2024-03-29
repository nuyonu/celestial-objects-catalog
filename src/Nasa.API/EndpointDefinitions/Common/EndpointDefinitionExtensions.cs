﻿namespace Nasa.API.EndpointDefinitions.Common;

public static class EndpointDefinitionExtensions
{
    public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpointDefinitions = new List<IEndpointDefinition>();

        foreach (var scanMarker in scanMarkers)
            endpointDefinitions.AddRange(scanMarker.Assembly.ExportedTypes
                .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>()
            );

        foreach (var endpointDefinition in endpointDefinitions) endpointDefinition.DefineServices(services);

        services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
    }

    public static void UseEndpointDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

        foreach (var endpointDefinition in definitions) endpointDefinition.DefineEndpoints(app);
    }
}