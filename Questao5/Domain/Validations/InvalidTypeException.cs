using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations;
public class InvalidTypeException : BaseException
{
    public InvalidTypeException(string error) : base(error, tipoException: TipoException.INVALID_TYPE)
    { }
}
