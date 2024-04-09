using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentoCreateCommand : IRequest<MovimentoCreateResponse>
    {
        public Guid IdRequisicao { get; set; }
        public Guid IdContaCorrente { get; set; }
        public double Valor { get; set; }
        public string TipoMovimento { get; set; }
        public MovimentoCreateCommand(Guid idRequisicao, Guid idContaCorrente, string tipoMovimento, double valor)
        {
            IdRequisicao = idRequisicao;
            IdContaCorrente = idContaCorrente;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            MovimentoCreateCommand other = (MovimentoCreateCommand)obj;
            return IdRequisicao == other.IdRequisicao &&
                   IdContaCorrente == other.IdContaCorrente &&
                   Valor == other.Valor &&
                   TipoMovimento == other.TipoMovimento;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(IdRequisicao, IdContaCorrente, Valor, TipoMovimento);
        }
    }
}
