using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations;
public abstract class BaseException : Exception
{
    public BaseException(string error, TipoException tipoException) : base(error)
    {
        TipoException = tipoException;
    }
    public TipoException TipoException { get; private set; }
}
