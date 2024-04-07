using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Responses;
public class MovimentoCreateResponse
{
    public MovimentoCreateResponse()
    {
        
    }
    public Guid IdMovimento { get; set; }

    public static explicit operator MovimentoCreateResponse(Movimento movimento)
    {
        if (movimento is null)
        {
            return null;
        }

        return new MovimentoCreateResponse()
        {
            IdMovimento = movimento.IdMovimento
        };
    }
}
