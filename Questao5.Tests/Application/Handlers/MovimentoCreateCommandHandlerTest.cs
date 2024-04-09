using MediatR;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Extensions;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Tests.Fixtures.Domain.Entities;

namespace Questao5.Tests.Application.Handlers;
public class MovimentoCreateCommandHandlerTest
{
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    private readonly IMediator _mediator;

    public MovimentoCreateCommandHandlerTest()
    {
        _idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        _movimentoRepository = Substitute.For<IMovimentoRepository>();
        _contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        _mediator = Substitute.For<IMediator>();
    }
    private void IniciarMocks(Movimento movimento, ContaCorrente contaCorrente)
    {
        _idempotenciaRepository.BuscarPorChaveIdempotencia(Arg.Any<Guid>())
                .Returns(Task.FromResult<Idempotencia>(null));

        _contaCorrenteRepository.BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>())
                .Returns(Task.FromResult(contaCorrente));

        _movimentoRepository.Salvar(Arg.Any<Movimento>())
            .Returns(Task.FromResult(movimento));

        _mediator.Send(Arg.Any<IdempotenciaCreateCommand>())
            .Returns<IdempotenciaCreateResponse>(new IdempotenciaCreateResponse());
    }


    [Fact(DisplayName = "Deve Concluir com Sucesso")]
    public async void Deve_Concluir_Com_Sucesso()
    {        
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        var movimento = (await MovimentoFixture.MovimentosExemplo1(contaCorrente)).First();
        IniciarMocks(movimento, contaCorrente);

        var handler = new MovimentoCreateCommandHandler(_movimentoRepository,
            _contaCorrenteRepository, _idempotenciaRepository, _mediator);

        Guid idRequisicao = Guid.NewGuid();
        var command = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(movimento.IdMovimento, result.IdMovimento);

        await _idempotenciaRepository.Received(1)
                .BuscarPorChaveIdempotencia(Arg.Any<Guid>());

        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.Received(1).Salvar(Arg.Any<Movimento>());
        await _mediator.Received(1).Send(Arg.Any<IdempotenciaCreateCommand>());
    }

    [Fact(DisplayName = "Deve Gerar Erro Conta Não Cadastrada")]
    public async void Deve_Gerar_Erro_Conta_Nao_Cadastrada()
    {
        //Arrange
        var contaCorrente = await ContaCorrenteFixture.ObterPrimeiro();
        var movimento = (await MovimentoFixture.MovimentosExemplo1(contaCorrente)).First();

        ContaCorrente contaCorrenteNULL = null;

        IniciarMocks(movimento, contaCorrenteNULL);

        var handler = new MovimentoCreateCommandHandler(_movimentoRepository,
            _contaCorrenteRepository, _idempotenciaRepository, _mediator);

        Guid idRequisicao = Guid.NewGuid();
        var command = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InvalidAccountException>(async () => await fnHandler());
        Assert.Equal("Apenas contas correntes cadastradas podem receber movimentação", exception.Message);

        await _idempotenciaRepository.Received(1)
                .BuscarPorChaveIdempotencia(Arg.Any<Guid>());

        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive().Salvar(Arg.Any<Movimento>());
        await _mediator.DidNotReceive().Send(Arg.Any<IdempotenciaCreateCommand>());
    }

    [Fact(DisplayName = "Deve Gerar Erro Conta Invalida")]
    public async void Deve_Gerar_Erro_Conta_Invalida()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterInativas()).First();
        var movimento = (await MovimentoFixture.MovimentosExemplo1(contaCorrente)).First();


        IniciarMocks(movimento, contaCorrente);

        var handler = new MovimentoCreateCommandHandler(_movimentoRepository,
            _contaCorrenteRepository, _idempotenciaRepository, _mediator);

        Guid idRequisicao = Guid.NewGuid();
        var command = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InactiveAccountException>(async () => await fnHandler());
        Assert.Equal("Apenas contas correntes ativas podem receber movimentação", exception.Message);

        await _idempotenciaRepository.Received(1)
                .BuscarPorChaveIdempotencia(Arg.Any<Guid>());

        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive().Salvar(Arg.Any<Movimento>());
        await _mediator.DidNotReceive().Send(Arg.Any<IdempotenciaCreateCommand>());
    }

    [Fact(DisplayName = "Deve Gerar Erro ao Salvar Idempotencia")]
    public async void Deve_Gerar_Erro_Salvar_Idempotencia()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        var movimento = (await MovimentoFixture.MovimentosExemplo1(contaCorrente)).First();
        IniciarMocks(movimento, contaCorrente);

        var _mediatorException = Substitute.For<IMediator>();
        _mediatorException.Send(Arg.Any<IdempotenciaCreateCommand>())
                             .ThrowsAsync<Exception>();

        var handler = new MovimentoCreateCommandHandler(_movimentoRepository,
            _contaCorrenteRepository, _idempotenciaRepository, _mediatorException);

        Guid idRequisicao = Guid.NewGuid();
        var command = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () => await fnHandler());
        Assert.Equal("Ocorreu um erro ao persistir a Idempotencia", exception.Message);

        await _idempotenciaRepository.Received(1)
                .BuscarPorChaveIdempotencia(Arg.Any<Guid>());

        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.Received(1).Salvar(Arg.Any<Movimento>());
    }


    [Fact(DisplayName = "Deve Retornar Idempotencia salva na tabela")]
    public async void Deve_Retornar_Idempotencia_Salva()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        var movimento = (await MovimentoFixture.MovimentosExemplo1(contaCorrente)).First();
        IniciarMocks(movimento, contaCorrente);

        Guid idRequisicao = Guid.Parse("4fdd27f5-9e1b-4670-baeb-aa8021044bbb");

        var commandDiferente = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor + 23.50);

        var idempotencia = new Idempotencia(chave_idempotencia: idRequisicao,
            requisicao: commandDiferente.ToJson(), resultado: (new { movimento.IdMovimento }).ToJson());

        var idempotenciaRepositoryMock = Substitute.For<IIdempotenciaRepository>();
        idempotenciaRepositoryMock.BuscarPorChaveIdempotencia(Arg.Any<Guid>())
                .Returns(Task.FromResult(idempotencia));

        var command = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor);

        var handler = new MovimentoCreateCommandHandler(_movimentoRepository,
            _contaCorrenteRepository, idempotenciaRepositoryMock, _mediator);


        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () => await fnHandler());
        Assert.Equal("Operação abortada, o Id Requisição já foi processado com corpo da requisão diferente", exception.Message);

        await idempotenciaRepositoryMock.Received(1)
                .BuscarPorChaveIdempotencia(Arg.Any<Guid>());

        await _contaCorrenteRepository.DidNotReceive()
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive().Salvar(Arg.Any<Movimento>());
        await _mediator.DidNotReceive().Send(Arg.Any<IdempotenciaCreateCommand>());
    }

    [Fact(DisplayName = "Deve Erro Idempotencia salva Diferente do Salvo em Banco de dados")]
    public async void Deve_Erro_Idempotencia_Diferente_Banco_Dados()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        var movimento = (await MovimentoFixture.MovimentosExemplo1(contaCorrente)).First();
        IniciarMocks(movimento, contaCorrente);

        Guid idRequisicao = Guid.Parse("4fdd27f5-9e1b-4670-baeb-aa8021044bbb");

        var command = new MovimentoCreateCommand(idRequisicao: idRequisicao,
                idContaCorrente: movimento.IdContaCorrente, tipoMovimento: ((char)movimento.TipoMovimento).ToString(),
                valor: movimento.Valor);

        var idempotencia = new Idempotencia(chave_idempotencia: idRequisicao,
            requisicao: command.ToJson(), resultado: (new { movimento.IdMovimento }).ToJson());

        var idempotenciaRepositoryMock = Substitute.For<IIdempotenciaRepository>();
        idempotenciaRepositoryMock.BuscarPorChaveIdempotencia(Arg.Any<Guid>())
                .Returns(Task.FromResult(idempotencia));

        var handler = new MovimentoCreateCommandHandler(_movimentoRepository,
            _contaCorrenteRepository, idempotenciaRepositoryMock, _mediator);


        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(movimento.IdMovimento, result.IdMovimento);

        await idempotenciaRepositoryMock.Received(1)
                .BuscarPorChaveIdempotencia(Arg.Any<Guid>());

        await _contaCorrenteRepository.DidNotReceive()
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive().Salvar(Arg.Any<Movimento>());
        await _mediator.DidNotReceive().Send(Arg.Any<IdempotenciaCreateCommand>());
    }

}
