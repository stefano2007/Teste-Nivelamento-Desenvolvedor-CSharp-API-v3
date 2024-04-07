using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests
{
    public class SaldoContaCorrenteViewSQLQuery : IRequest<SaldoContaCorrenteResponse>
    {
        public SaldoContaCorrenteViewSQLQuery(Guid idContaCorrente)
        {
            IdContaCorrente = idContaCorrente;
        }
        public Guid IdContaCorrente { get; set; }
    }
}
