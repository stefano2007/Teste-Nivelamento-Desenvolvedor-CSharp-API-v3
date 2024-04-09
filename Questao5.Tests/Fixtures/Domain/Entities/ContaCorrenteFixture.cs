using Questao5.Domain.Entities;

namespace Questao5.Tests.Fixtures.Domain.Entities;
public static class ContaCorrenteFixture
{
    public static Task<IEnumerable<ContaCorrente>> ObterTodas()
    {
        var contaCorrentes = new List<ContaCorrente>();

        var contaCorrente1 = new ContaCorrente(Guid.Parse("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), 123, "Katherine Sanchez");
        var contaCorrente2 = new ContaCorrente(Guid.Parse("FA99D033-7067-ED11-96C6-7C5DFA4A16C9"), 456, "Eva Woodward");
        var contaCorrente3 = new ContaCorrente(Guid.Parse("382D323D-7067-ED11-8866-7D5DFA4A16C9"), 789, "Tevin Mcconnell");

        contaCorrentes.Add(contaCorrente1);
        contaCorrentes.Add(contaCorrente2);
        contaCorrentes.Add(contaCorrente3);

        var contaCorrente4 = new ContaCorrente(Guid.Parse("F475F943-7067-ED11-A06B-7E5DFA4A16C9"), 741, "Ameena Lynn");
        var contaCorrente5 = new ContaCorrente(Guid.Parse("BCDACA4A-7067-ED11-AF81-825DFA4A16C9"), 852, "Jarrad Mckee");
        var contaCorrente6 = new ContaCorrente(Guid.Parse("D2E02051-7067-ED11-94C0-835DFA4A16C9"), 963, "Elisha Simons");

        contaCorrente4.Inativar();
        contaCorrente5.Inativar();
        contaCorrente6.Inativar();

        contaCorrentes.Add(contaCorrente4);
        contaCorrentes.Add(contaCorrente5);
        contaCorrentes.Add(contaCorrente6);

        return Task.FromResult<IEnumerable<ContaCorrente>>(contaCorrentes);
    }

    public static async Task<IEnumerable<ContaCorrente>> ObterAtivos()
        => (await ObterTodas()).Where(cc => cc.Ativo);

    public static async Task<IEnumerable<ContaCorrente>> ObterInativas()
        => (await ObterTodas()).Where(cc => !cc.Ativo);


    public async static Task<ContaCorrente> ObterPrimeiro()
    {
        return (await ObterTodas()).First(); 
    }
}
