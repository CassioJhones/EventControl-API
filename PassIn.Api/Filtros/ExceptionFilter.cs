using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Application.LogFiles;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using System.Net;

namespace PassIn.Api.Filtros;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        bool result = context.Exception is PassInException;
        if (result) HandleProjectException(context);
        else ThrowUnknowError(context);

    }

    private void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            Log.LogToFile("Nao Encontrado", $"{context.Exception.Message} - {context.Exception.Source}");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = new NotFoundObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
        else if (context.Exception is ErrorOnValidationException)
        {
            Log.LogToFile("Erro de Validação", $"{context.Exception.Message} - {context.Exception.Source}");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
        else if (context.Exception is ConflictException)
        {
            Log.LogToFile("Erro de Conflito", $"{context.Exception.Message} - {context.Exception.Source}");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Result = new ConflictObjectResult(new ResponseErrorJson(context.Exception.Message));
        }
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        Log.LogToFile("Erro Desconhecido", $"{context.Exception.Message} - {context.Exception.Source}");
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson("Erro Desconhecido"));
    }
}

