using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers
{
    public class GetIdempotenciaByChaveIdempotenciaQueryHandler : IRequestHandler<GetIdempotenciaByChaveIdempotenciaQuery, IdempotenciaResponse>
    {
        private readonly IIdempotenciaRepository _idempotenciaCorrenteRepository;

        public GetIdempotenciaByChaveIdempotenciaQueryHandler(IIdempotenciaRepository idempotenciaCorrenteRepository)
        {
            _idempotenciaCorrenteRepository = idempotenciaCorrenteRepository;
        }

        public async Task<IdempotenciaResponse> Handle(GetIdempotenciaByChaveIdempotenciaQuery request, CancellationToken cancellationToken)
        {
            return (IdempotenciaResponse) await _idempotenciaCorrenteRepository.BuscarPorChaveIdempotencia(request.ChaveIdempotencia);
        }
    }
}
