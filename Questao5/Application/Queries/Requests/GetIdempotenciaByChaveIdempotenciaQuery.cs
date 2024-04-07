using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class GetIdempotenciaByChaveIdempotenciaQuery : IRequest<IdempotenciaResponse>
    {
        public GetIdempotenciaByChaveIdempotenciaQuery(Guid chaveIdempotencia)
        {
            ChaveIdempotencia = chaveIdempotencia;
        }
        public Guid ChaveIdempotencia { get; set; }
    }
}
