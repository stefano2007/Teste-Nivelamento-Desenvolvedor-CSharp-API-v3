using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class GetContaCorrenteByIdContaCorrenteQuery : IRequest<ContaCorrenteResponse>
    {
        public GetContaCorrenteByIdContaCorrenteQuery(Guid idContaCorrente, bool incluirMovimentos)
        {
            IdContaCorrente = idContaCorrente;
            IncluirMovimentos = incluirMovimentos;
        }
        public Guid IdContaCorrente { get; set; }
        public bool IncluirMovimentos { get; set; }
    }
}
