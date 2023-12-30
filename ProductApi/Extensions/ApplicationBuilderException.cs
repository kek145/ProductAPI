using Microsoft.AspNetCore.Builder;
using ProductApi.Middlewares;

namespace ProductApi.Extensions;

public static class ApplicationBuilderException
{
    public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<ErrorHandlerMiddleware>();
}