using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validation;
public class InvalidTypeException : BaseException
{
    public InvalidTypeException(string error) : base(error, tipoException: TipoException.INVALID_TYPE)
    {
    }
}
