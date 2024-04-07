using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Questao5.Domain.Validation;
using Questao5.Infrastructure.Services.Model;

namespace Questao5.Infrastructure.Configurations;
public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case BaseException:
                var exception = (BaseException)context.Exception;
                context.Result = new BadRequestObjectResult(new CustomErro(mensagem: exception.Message, tipoException: exception.TipoException));
                break;
            default:
                context.Result = new BadRequestObjectResult("Ocorreu um erro desconhecido");
            break;
        };
    }
}
