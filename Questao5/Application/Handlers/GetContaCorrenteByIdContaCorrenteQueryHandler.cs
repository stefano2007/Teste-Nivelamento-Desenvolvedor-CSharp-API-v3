using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers
{
    public class GetContaCorrenteByIdContaCorrenteQueryHandler : IRequestHandler<GetContaCorrenteByIdContaCorrenteQuery, ContaCorrenteResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public GetContaCorrenteByIdContaCorrenteQueryHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ContaCorrenteResponse> Handle(GetContaCorrenteByIdContaCorrenteQuery request, CancellationToken cancellationToken)
        {
            return (ContaCorrenteResponse) await _contaCorrenteRepository
                    .BuscarPorIdContaCorrente(request.IdContaCorrente, 
                        request.IncluirMovimentos
                    );
        }
    }
}
