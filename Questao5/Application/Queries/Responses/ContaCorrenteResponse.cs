using Questao5.Domain.Entities;

namespace Questao5.Application.Queries.Responses
{
    public class ContaCorrenteResponse
    {
        public Guid IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<MovimentoResponse> Movimentos { get; set; }

        public static explicit operator ContaCorrenteResponse(ContaCorrente contaCorrente)
        {
            if (contaCorrente is null)
            {
                return null;
            }

            return new ContaCorrenteResponse()
            {
                IdContaCorrente = contaCorrente.IdContaCorrente,
                Numero = contaCorrente.Numero,
                Nome = contaCorrente.Nome,
                Ativo = contaCorrente.Ativo,
                Movimentos = contaCorrente.Movimentos is not null
                    ? contaCorrente.Movimentos.Select(mov => (MovimentoResponse)mov)
                    : new List<MovimentoResponse>()
            };
        }
    }
}
