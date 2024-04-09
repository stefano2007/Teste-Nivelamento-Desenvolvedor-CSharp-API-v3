using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations;
public class InvalidAccountException : BaseException
{
    public InvalidAccountException(string error) : base(error, tipoException: TipoException.INVALID_ACCOUNT)
    { }
}
