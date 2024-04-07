using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Responses;
public class IdempotenciaResponse
{
    public Guid ChaveIdempotencia { get; set; }
    public string? Requisicao { get; set; }
    public string? Resultado { get; set; }

    public static explicit operator IdempotenciaResponse(Idempotencia idempotencia)
    {
        if (idempotencia is null)
        {
            return null;
        }

        return new IdempotenciaResponse()
        {
            ChaveIdempotencia = idempotencia.Chave_Idempotencia,
            Requisicao = idempotencia.Requisicao,
            Resultado = idempotencia.Resultado
        };
    }
}
