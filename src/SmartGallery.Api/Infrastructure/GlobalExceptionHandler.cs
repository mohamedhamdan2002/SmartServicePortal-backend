﻿using Microsoft.AspNetCore.Diagnostics;
using SmartGallery.Core.Errors;

namespace SmartGallery.Api.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(ApplicationErrors.InternalServerError, cancellationToken);
            return true;
        }
    }
}
