namespace Questao5.Application.Queries.Responses;
public class SaldoContaCorrenteResponse
{    
    public int Numero { get; set; }
    public string NomeTitular { get; set; }
    public DateTime DataResposta { get; set; }
    public double ValorSaldoAtual { get; set; }
    public static SaldoContaCorrenteResponse CriarResponse(int numero, string nomeTitular, double valorSaldoAtual)
    {
        return new SaldoContaCorrenteResponse
        {
            Numero = numero,
            NomeTitular = nomeTitular,
            DataResposta = DateTime.Now,
            ValorSaldoAtual = valorSaldoAtual
        };
    }
}
