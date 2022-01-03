using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Json;
using Nasa.API.EndpointDefinitions;
using Nasa.API.EndpointDefinitions.Common;
using Nasa.Application;
using Nasa.Infrastructure;
using Nasa.Shared;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinitionsMarker));

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.RegisterApplication();

builder.Services.RegisterInfrastructure();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseEndpointDefinitions();
    
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        var exception = exceptionHandlerPathFeature?.Error;
        
        var code = StatusCodes.Status500InternalServerError;
        var errors = new List<string> { exception.Message };

        code = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            _ => code
        };

        var result = JsonConvert.SerializeObject(CommandResponse<string>.Fail(errors));

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = code;

        await context.Response.WriteAsync(result);
    });
});

app.Run();