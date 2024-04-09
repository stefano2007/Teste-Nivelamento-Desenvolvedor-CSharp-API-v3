using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Validations;
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
            case ArgumentException:
            case InvalidOperationException:
                context.Result = new BadRequestObjectResult(new CustomErro(mensagem: context.Exception.Message, tipoException: TipoException.BAD_REQUEST));
                break;
            default:
                context.Result = new BadRequestObjectResult(new CustomErro(mensagem: "Ocorreu um erro desconhecido", tipoException: TipoException.INTERNAL_SERVER_ERROR));
            break;
        };
    }
}
