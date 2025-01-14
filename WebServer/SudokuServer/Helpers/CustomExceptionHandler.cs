using System.Net.Mime;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Diagnostics;

namespace SudokuServer.Helpers;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger,
    ProblemDetailsFactory problemDetailsFactory) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
        Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Something went wrong");

        var problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext, 
            statusCode: StatusCodes.Status500InternalServerError, 
            detail: exception.Message);

        httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;
        httpContext.Response.StatusCode = (int)(problemDetails.Status is null ? StatusCodes.Status500InternalServerError : problemDetails.Status);

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
