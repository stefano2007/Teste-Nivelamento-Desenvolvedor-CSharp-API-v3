using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Responses;

public class IdempotenciaCreateResponse
{
    public Guid ChaveIdempotencia { get; set; }
    public string? Requisicao { get; set; }
    public string? Resultado { get; set; }

    public static explicit operator IdempotenciaCreateResponse(Idempotencia idempotencia)
    {
        if (idempotencia is null)
        {
            return null;
        }

        return new IdempotenciaCreateResponse()
        {
            ChaveIdempotencia = idempotencia.Chave_Idempotencia,
            Requisicao = idempotencia.Requisicao,
            Resultado = idempotencia.Resultado
        };
    }
}
