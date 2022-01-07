using System.Net.Mime;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Nasa.Shared.Application;
using Newtonsoft.Json;

namespace Nasa.API;

public static class WebApplicationExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var exception = exceptionHandlerPathFeature?.Error;

                int code;
                IEnumerable<string> errors;

                switch (exception)
                {
                    case ValidationException validationException:
                    {
                        code = StatusCodes.Status400BadRequest;
                        errors = validationException.Errors.Select(c => c.ErrorMessage);

                        break;
                    }
                    default:
                    {
                        code = StatusCodes.Status500InternalServerError;
                        errors = new List<string> { "Something went wrong" };

                        break;
                    }
                }

                var result = JsonConvert.SerializeObject(CommandResponse<string>.Fail(errors));

                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = code;

                await context.Response.WriteAsync(result);
            });
        });
    }
}