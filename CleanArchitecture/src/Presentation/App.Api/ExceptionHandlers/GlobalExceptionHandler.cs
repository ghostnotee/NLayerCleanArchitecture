using System.Net;
using App.Application;
using Microsoft.AspNetCore.Diagnostics;

namespace App.Api.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var errorAsDto = ServiceResult.Failure(exception.Message, HttpStatusCode.InternalServerError);
        httpContext.Response.StatusCode = (int)errorAsDto.StatusCode;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsJsonAsync(errorAsDto, cancellationToken);

        return true;
    }
}