using LocationSearch.Common.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace BankPlatform.Api.Extenstions
{
    public static class ExceptionMiddlewareExtenstion
    {
        public static void ConfigureErrorHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        logger.LogError(contextFeature.Error, contextFeature.Error.Message);
                        await context.Response.WriteAsync((new ErrorDetail { Message = "Interal server error", StatusCode = context.Response.StatusCode }).ToString());
                    }
                });
            });
        }
    }
}
