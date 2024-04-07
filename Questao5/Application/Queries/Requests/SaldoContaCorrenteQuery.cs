using MediatR;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Queries.Requests;
public class SaldoContaCorrenteQuery : IRequest<SaldoContaCorrenteResponse>
{
    public SaldoContaCorrenteQuery(Guid idContaCorrente)
    {
        IdContaCorrente = idContaCorrente;
    }
    public Guid IdContaCorrente { get; set; }
}
