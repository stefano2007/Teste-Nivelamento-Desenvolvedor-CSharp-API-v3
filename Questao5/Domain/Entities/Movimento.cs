using Questao5.Domain.Enumerators;
using Questao5.Domain.Validations;

namespace Questao5.Domain.Entities;
public class Movimento
{
    public Movimento() { }
    public Movimento(Guid idContaCorrente, string tipoMovimento, double valor)
    {
        ValidateDomain(idContaCorrente, tipoMovimento, valor);
        DataMovimento = DateTime.Now;
        IdMovimento = Guid.NewGuid();
    }
    public Guid IdMovimento { get; private set; }
    public Guid IdContaCorrente { get; private set; }
    public DateTime DataMovimento { get; private set; }
    public TipoMovimento TipoMovimento { get; private set; }
    public double Valor { get; private set; }
    public bool EhDebito () => TipoMovimento.Equals(Enumerators.TipoMovimento.Debito);
    public ContaCorrente ContaCorrente { get; private set; }
    private void ValidateDomain(Guid idContaCorrente, string tipoMovimento, double valor)
    {
        if (valor <= 0)
        {
            throw new InvalidValueException("Apenas valores positivos podem ser recebidos");
        }

        TipoMovimento tipoMovimentoEnum;
        const string erroTipoMovimento = "Apenas os tipos D=“débito” ou C=“crédito” podem ser aceitos";
        try
        {
            tipoMovimentoEnum = (Enumerators.TipoMovimento) Convert.ToChar(tipoMovimento);
        }
        catch
        {
            throw new InvalidTypeException(erroTipoMovimento);
        }
        if (tipoMovimentoEnum != Enumerators.TipoMovimento.Credito && tipoMovimentoEnum != Enumerators.TipoMovimento.Debito)
        {
            throw new InvalidTypeException(erroTipoMovimento);
        }
        
        IdContaCorrente = idContaCorrente;
        TipoMovimento = tipoMovimentoEnum;
        Valor = valor;
    }
}
