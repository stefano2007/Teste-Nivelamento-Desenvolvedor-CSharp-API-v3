using Questao5.Domain.Entities;

namespace Questao5.Tests.Fixtures.Domain.Entities;
public class MovimentoFixture
{
    public static Task<Movimento> ObterMovimento(ContaCorrente contaCorrente,
        string tipoMovimento, double valor)
        => Task.FromResult(new Movimento(idContaCorrente: contaCorrente.IdContaCorrente,
            tipoMovimento: tipoMovimento, valor: valor));

    public static async Task<IEnumerable<Movimento>> MovimentosExemplo1(ContaCorrente contaCorrente)
    {
        var movimentos = new List<Movimento>();
        var mov1 = await ObterMovimento(contaCorrente, "C", 350.0);
        var mov2 = await ObterMovimento(contaCorrente, "C", 200.0);
        var mov3 = await ObterMovimento(contaCorrente, "D", 202.5);

        movimentos.Add(mov1);
        movimentos.Add(mov2);
        movimentos.Add(mov3);

        return movimentos;
    }
    public static async Task<IEnumerable<Movimento>> MovimentosExemplo2(ContaCorrente contaCorrente)
    {
        var movimentos = new List<Movimento>();
        var mov1Exemplo2 = await ObterMovimento(contaCorrente, "C", 300.0);
        var mov2Exemplo2 = await ObterMovimento(contaCorrente, "D", 301.5);

        movimentos.Add(mov1Exemplo2);
        movimentos.Add(mov2Exemplo2);

        return movimentos;
    }
    public static async Task SetMovimentos(ContaCorrente contaCorrente, bool exemplo1 = true)
    {
        contaCorrente.SetMovimentos(exemplo1
            ? await MovimentosExemplo1(contaCorrente) 
            : await MovimentosExemplo2(contaCorrente));
    }
}
