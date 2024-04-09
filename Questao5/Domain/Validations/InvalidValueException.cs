using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations;
public class InvalidValueException : BaseException
{
    public InvalidValueException(string error) : base(error, tipoException: TipoException.INVALID_VALUE)
    { }
}
