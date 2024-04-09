using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations;
public class InactiveAccountException : BaseException
{
    public InactiveAccountException(string error) : base(error, tipoException: TipoException.INACTIVE_ACCOUNT)
    { }
}
