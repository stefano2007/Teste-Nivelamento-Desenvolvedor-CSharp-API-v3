using Questao5.Application.Commands.Responses;
using Questao5.Domain.Extensions;
namespace Questao5.Tests.Domain.Extensions;
public class JsonExtensionTest
{
    Guid guidIdMovimento = Guid.Parse("4fdd27f5-9e1b-4670-baeb-aa8021044bbb");

    public MovimentoCreateResponse ObterMovimentoCreateCommand()
    {
       return new() { IdMovimento = guidIdMovimento };
    }

    [Fact(DisplayName = "Deve converte o Objeto em Json e desconverter com sucesso")]
    public void Deve_Converter_Com_Sucesso()
    {
        //Arrange
        var command = ObterMovimentoCreateCommand();

        //Act
        var jsonConvertido = command.ToJson();

        var commandDesconvertido = jsonConvertido.ToObjetoByJson<MovimentoCreateResponse>();
        //Assert
        Assert.NotNull(jsonConvertido);
        Assert.Equal("{\"IdMovimento\":\"4fdd27f5-9e1b-4670-baeb-aa8021044bbb\"}", jsonConvertido);

        Assert.NotNull(commandDesconvertido);
        Assert.Equal(command.IdMovimento, commandDesconvertido.IdMovimento);
    }

    [Fact(DisplayName = "Deve converte o Objeto em Json e desconverter diferentes")]
    public void Deve_Converter_Diferente()
    {
        //Arrange
        var command = ObterMovimentoCreateCommand();

        //Act
        var jsonConvertido = command.ToJson();
        var commandDesconvertido = jsonConvertido.ToObjetoByJson<MovimentoCreateResponse>();

        command.IdMovimento = Guid.NewGuid();
        //Assert
        Assert.NotNull(jsonConvertido);
        Assert.Equal("{\"IdMovimento\":\"4fdd27f5-9e1b-4670-baeb-aa8021044bbb\"}", jsonConvertido);

        Assert.NotNull(commandDesconvertido);
        Assert.NotEqual(command.IdMovimento, commandDesconvertido.IdMovimento);
    }

}
