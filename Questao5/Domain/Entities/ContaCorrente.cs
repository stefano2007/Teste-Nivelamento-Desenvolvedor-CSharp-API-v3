using Questao5.Domain.Validations;

namespace Questao5.Domain.Entities;
public class ContaCorrente
{
    public ContaCorrente()
    {
        
    }
    public ContaCorrente(Guid idContaCorrente, int numero, string nome)
    {
        ValidateDomain(idContaCorrente, numero, nome);
        Ativo = true;
    }

    public Guid IdContaCorrente { get; private set; }
    public int Numero { get; private set; }
    public string Nome { get; private set; }
    public bool Ativo { get; private set; }
    public void Inativar()
    {
        Ativo = false;
    }
    private IEnumerable<Movimento> _movimentos { get; set; }
    public IEnumerable<Movimento> Movimentos { get => _movimentos; }    
    public void SetMovimentos(IEnumerable<Movimento> movimentos) => _movimentos = movimentos;

    /*
    idcontacorrente TEXT(37) PRIMARY KEY, -- id da conta corrente
	    numero INTEGER(10) NOT NULL UNIQUE*, -- numero da conta corrente
	    nome TEXT(100) NOT NULL, -- nome do titular da conta corrente
	    ativo INTEGER(1) NOT NULL default 0, -- indicativo se a conta esta ativa. (0 = inativa, 1 = ativa).
	    CHECK (ativo in (0,1)*
     */

    public double SaldoContaCorrente()
    {
        _movimentos ??= new List<Movimento>();
        return _movimentos
            .Sum(mov => mov.TipoMovimento.Equals("D")
                ? mov.Valor * -1
                : mov.Valor
            );
    }

    public void ValidateDomain(Guid idContaCorrente, int numero, string nome)
    {
        /*
        DomainExceptionValidation.When(string.IsNullOrEmpty(idContaCorrente),
            "Invalid Id Conta Corrente is requerid");

        DomainExceptionValidation.When(idContaCorrente.Length > 37,
            "Invalid Id Conta Corrente maximum de 37 caracters");
        */
        DomainExceptionValidation.When(numero <= 0,
            "Invalid Valor is requerid");

        DomainExceptionValidation.When(string.IsNullOrEmpty(nome),
            "Invalid Nome is requerid");

        DomainExceptionValidation.When(nome.Length > 100,
            "Invalid Nome maximum de 100 caracters");

        IdContaCorrente = idContaCorrente;
        Numero = numero;
        Nome = nome;
    }
}
