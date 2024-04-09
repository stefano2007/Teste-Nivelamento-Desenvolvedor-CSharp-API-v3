using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Database.Repository;
using Questao5.Tests.Fixtures.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao5.Tests.Application.Handlers;
public class IdempotenciaCreateCommandHandlerTest
{
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    public IdempotenciaCreateCommandHandlerTest()
    {
        _idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
    }
    public void IniciarMocks(Idempotencia idempotencia)
    {
        _idempotenciaRepository.Salvar(Arg.Any<Idempotencia>())
                .Returns(Task.FromResult(idempotencia));
    }

    [Fact(DisplayName = "Deve Concluir com Sucesso")]
    public async void Deve_Concluir_Com_Sucesso()
    {
        //Arrange
        var idempotencia = new Idempotencia(chave_idempotencia: Guid.NewGuid(),
            requisicao: "teste", resultado: "teste");

        IniciarMocks(idempotencia);

        var handler = new IdempotenciaCreateCommandHandler(_idempotenciaRepository);

        var command = new IdempotenciaCreateCommand(chaveIdempotencia: idempotencia.Chave_Idempotencia,
            requisicao: idempotencia.Requisicao, resultado: idempotencia.Resultado);               

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(command.ChaveIdempotencia, result.ChaveIdempotencia);
        Assert.Equal(command.Requisicao, result.Requisicao);
        Assert.Equal(command.Resultado, result.Resultado);

        await _idempotenciaRepository.Received(1)
              .Salvar(Arg.Any<Idempotencia>());

    }
}
