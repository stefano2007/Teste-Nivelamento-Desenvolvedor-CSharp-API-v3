using Questao5.Domain.Validation;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Validations;
public class DomainExceptionValidation : BaseException
{
    public DomainExceptionValidation(string error) : base(error, tipoException: TipoException.DOMAIN_EXCEPTION)
    { }

    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainExceptionValidation(error);
    }
}
