using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoCreateCommand : IRequest<MovimentoCreateResponse>
    {
        public Guid IdRequisicao { get; set; }
        public Guid IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public char TipoMovimento { get; set; }
        public MovimentoCreateCommand(Guid idRequisicao, Guid idContaCorrente, char tipoMovimento, double valor)
        {
            IdRequisicao = idRequisicao;
            IdContaCorrente = idContaCorrente;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }
    }
}
