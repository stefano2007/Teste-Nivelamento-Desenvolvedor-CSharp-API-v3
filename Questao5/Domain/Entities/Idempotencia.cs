namespace Questao5.Domain.Entities;
public class Idempotencia
{
    public Idempotencia()
    {
        
    }
    public Idempotencia(Guid chave_idempotencia, string requisicao, string resultado)
    {
        Chave_Idempotencia = chave_idempotencia;
        Requisicao = requisicao;
        Resultado = resultado;
    }

    public Guid Chave_Idempotencia { get; private set; }
    public string Requisicao { get; private set; }
    public string Resultado { get; private set; }
}
