using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Nasa.Application.Common.Behaviors;
using Nasa.Application.Markers;

namespace Nasa.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(IValidationsMarker));

        services.AddMediatR(typeof(IMediatorMarker));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}