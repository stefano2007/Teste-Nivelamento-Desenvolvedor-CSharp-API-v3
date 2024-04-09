using Questao5.Domain.Entities;
using Questao5.Domain.Validations;

namespace Questao5.Tests.Domain.Entities;
public class ContaCorrenteTest
{
    [Fact(DisplayName = "Deve Criar o Objeto com Sucesso")]
    public void Deve_Criar_Com_Sucesso()
    {
        //Arrange e Act
        var contaCorrente = new ContaCorrente(Guid.Parse("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), 123, "Katherine Sanchez");

        //Assert
        Assert.NotNull(contaCorrente);
        Assert.Equal(123, contaCorrente.Numero);
    }

    [Fact(DisplayName = "Deve Erro numero Invalido")]
    public void Deve_Erro_Numero_Invalido()
    {
        //Arrange e Act 
        var fnContaCorrente = () => new ContaCorrente(Guid.Parse("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), 0, "Katherine Sanchez");

        //Assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => fnContaCorrente());
        Assert.Equal("O Numero é requerido", exception.Message);
    }

    [Fact(DisplayName = "Deve Erro Nome Invalido")]
    public void Deve_Erro_Nome_Requerido()
    {
        //Arrange e Act 
        var fnContaCorrente = () => new ContaCorrente(Guid.Parse("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), 123, "");

        //Assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => fnContaCorrente());
        Assert.Equal("O Nome é requerido", exception.Message);
    }

    [Fact(DisplayName = "Deve Erro Nome maior que 100 caracteres")]
    public void Deve_Erro_Nome_Limite_Maximo()
    {
        //Arrange e Act 
        var nome = "Lorem Ipsum é simplesmente uma simulação de texto da indústria "+
                "tipográfica e de impressos, e vem sendo utilizado desde o século XVI...";

        var fnContaCorrente = () => new ContaCorrente(Guid.Parse("B6BAFC09-6967-ED11-A567-055DFA4A16C9"), 123, nome);

        //Assert
        var exception = Assert.Throws<DomainExceptionValidation>(() => fnContaCorrente());
        Assert.Equal("O Nome deve ter no máximo 100 caracteres", exception.Message);
    }
}
