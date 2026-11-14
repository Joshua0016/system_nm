using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaFacturacion.App.Exceptions;

namespace SistemaFacturacion.Api.ExceptionHandling;

// Traduce las excepciones de App/Domain a respuestas ProblemDetails; el resto sigue como 500
public class AppExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public AppExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
            ConflictException => (StatusCodes.Status409Conflict, "Conflicto"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Solicitud inválida"),
            _ => (0, string.Empty)
        };

        if (status == 0)
        {
            return false;
        }

        httpContext.Response.StatusCode = status;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = GetDetail(exception)
            }
        });
    }

    // ArgumentException añade " (Parameter 'x')" al mensaje; el cliente solo necesita el texto
    private static string GetDetail(Exception exception)
    {
        return exception is ArgumentException { ParamName: not null } argumentException
            ? argumentException.Message.Replace($" (Parameter '{argumentException.ParamName}')", string.Empty)
            : exception.Message;
    }
}
