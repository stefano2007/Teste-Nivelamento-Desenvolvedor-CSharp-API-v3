using Questao5.Domain.Validations;

namespace Questao5.Domain.Entities;
public class ContaCorrente
{
    public ContaCorrente() { }
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

    public double SaldoContaCorrente()
    {
        _movimentos ??= new List<Movimento>();
        return _movimentos
            .Sum(mov => mov.EhDebito()
                ? mov.Valor * -1
                : mov.Valor
            );
    }
    public void ValidateDomain(Guid idContaCorrente, int numero, string nome)
    {
        DomainExceptionValidation.When(numero <= 0,
            "O Numero é requerido");

        DomainExceptionValidation.When(string.IsNullOrEmpty(nome),
            "O Nome é requerido");

        DomainExceptionValidation.When(nome.Length > 100,
            "O Nome deve ter no máximo 100 caracteres");

        IdContaCorrente = idContaCorrente;
        Numero = numero;
        Nome = nome;
    }
}
