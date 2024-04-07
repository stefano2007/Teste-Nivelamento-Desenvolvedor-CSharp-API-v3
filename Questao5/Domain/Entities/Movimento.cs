using Questao5.Domain.Enumerators;
using Questao5.Domain.Validation;
using Questao5.Domain.Validations;

namespace Questao5.Domain.Entities;
public class Movimento
{
    public Movimento()
    {
        
    }
    public Movimento(Guid idContaCorrente, char tipoMovimento, double valor)
    {
        ValidateDomain(idContaCorrente, tipoMovimento, valor);
        DataMovimento = DateTime.Now;
        IdMovimento = Guid.NewGuid();
    }
    public Guid IdMovimento { get; private set; }
    public Guid IdContaCorrente { get; private set; }
    public DateTime DataMovimento { get; private set; }
    public string TipoMovimento { get; private set; }
    public double Valor { get; private set; }
    public ContaCorrente ContaCorrente { get; private set; }

    private void ValidateDomain(Guid idContaCorrente, char tipoMovimento, double valor)
    {
        if (valor <= 0)
        {
            throw new InvalidValueException("Apenas valores positivos podem ser recebidos");
        }

        TipoMovimento tipoMovimentoEnum;
        try
        {
            tipoMovimentoEnum = (Enumerators.TipoMovimento) tipoMovimento;
        }
        catch
        {
            throw new InvalidTypeException("Apenas os tipos “débito” ou “crédito” podem ser aceitos");
        }
        if (tipoMovimentoEnum != Enumerators.TipoMovimento.Credito && tipoMovimentoEnum != Enumerators.TipoMovimento.Debito)
        {
            throw new InvalidTypeException("Apenas os tipos “débito” ou “crédito” podem ser aceitos");
        }

        DomainExceptionValidation.When(valor <= 0,
            "Invalid Valor is requerid");
        
        IdContaCorrente = idContaCorrente;
        TipoMovimento = tipoMovimento.ToString();//TODO: Resolver Enum
        Valor = valor;
    }
}
