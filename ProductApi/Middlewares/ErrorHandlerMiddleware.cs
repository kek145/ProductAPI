using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.BLL.Exceptions;

namespace ProductApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string message;
        HttpStatusCode statusCode;

        var exceptionType = ex.GetType();

        if (exceptionType == typeof(NotImplementedException))
        {
            message = $"Not implemented error: {ex.Message}";
            statusCode = HttpStatusCode.NotImplemented;
        }
        
        else if (exceptionType == typeof(BadRequestException))
        {
            message = $"BadRequest error: {ex.Message}";
            statusCode = HttpStatusCode.BadRequest;
        }
        
        else if (exceptionType == typeof(NotFoundException))
        {
            message = $"NotFound error: {ex.Message}";
            statusCode = HttpStatusCode.NotFound;
        }
        
        else if (exceptionType == typeof(DbUpdateException))
        {
            message = $"Database error: {ex}";
            statusCode = HttpStatusCode.InternalServerError;
        }
        
        else
        {
            message = $"Another type of error: {ex}";
            statusCode = HttpStatusCode.InternalServerError;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        
        return context.Response.WriteAsync(exceptionResult);
    }
}