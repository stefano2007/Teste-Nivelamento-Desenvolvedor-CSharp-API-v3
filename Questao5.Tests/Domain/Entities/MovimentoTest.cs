using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Validations;
using Questao5.Tests.Fixtures.Domain.Entities;

namespace Questao5.Tests.Domain.Entities;
public class MovimentoTest
{
    [Fact(DisplayName = "Deve Criar o Objeto com Sucesso")]
    public async void Deve_Criar_Com_Sucesso()
    {
        //Arrange 
        var contaCorrente = await ContaCorrenteFixture.ObterPrimeiro();

        //Act
        var movimento = new Movimento(idContaCorrente: contaCorrente.IdContaCorrente, tipoMovimento: "D", valor: 50.75);

        //Assert
        Assert.NotNull(movimento);
        Assert.Equal(TipoMovimento.Debito, movimento.TipoMovimento);
    }

    [Fact(DisplayName = "Deve Erro Valor Invalido")]
    public async void Deve_Erro_Numero_Invalido()
    {
        //Arrange
        var contaCorrente =  await ContaCorrenteFixture.ObterPrimeiro();
        //Act 
        var fnMovimento = () => new Movimento(idContaCorrente: contaCorrente.IdContaCorrente, tipoMovimento: "D", valor: 0);

        //Assert
        var exception = Assert.Throws<InvalidValueException>(() => fnMovimento());
        Assert.Equal("Apenas valores positivos podem ser recebidos", exception.Message);
    }

    [Fact(DisplayName = "Deve Erro Tipo Movimento Invalido")]
    public async void Deve_Erro_Nome_Requerido()
    {
        //Arrange
        var contaCorrente = await ContaCorrenteFixture.ObterPrimeiro();
        //Act 
        var fnMovimento = () => new Movimento(idContaCorrente: contaCorrente.IdContaCorrente, tipoMovimento: "X", valor: 10);

        //Assert
        var exception = Assert.Throws<InvalidTypeException>(() => fnMovimento());
        Assert.Equal("Apenas os tipos D=“débito” ou C=“crédito” podem ser aceitos", exception.Message);
    }
}
