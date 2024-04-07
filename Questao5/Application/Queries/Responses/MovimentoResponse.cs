using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Responses;
public class MovimentoResponse
{
    public Guid IdMovimento { get; set; }
    public Guid IdContaCorrente { get; set; }
    public DateTime DataMovimento { get; set; }
    public string TipoMovimento { get; set; }
    public double Valor { get; set; }

    public static explicit operator MovimentoResponse(Movimento movimento)
    {
        if (movimento is null)
        {
            return null;
        }

        return new MovimentoResponse()
        {
            IdMovimento = movimento.IdMovimento,
            IdContaCorrente = movimento.IdContaCorrente,
            DataMovimento = movimento.DataMovimento,
            TipoMovimento = movimento.TipoMovimento,
            Valor = movimento.Valor
        };
    }
}
