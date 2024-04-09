using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Entities;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Tests.Fixtures.Domain.Entities;

namespace Questao5.Tests.Application.Handlers;
public class SaldoContaCorrenteQueryHandlerTest
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMovimentoRepository _movimentoRepository;
    public SaldoContaCorrenteQueryHandlerTest()
    {
        _contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        _movimentoRepository = Substitute.For<IMovimentoRepository>();
    }

    public void IniciarMocks(ContaCorrente contaCorrente)
    {
        _contaCorrenteRepository.BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>())
                .Returns(Task.FromResult(contaCorrente));

        _movimentoRepository.BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>())
            .Returns(Task.FromResult(contaCorrente.SaldoContaCorrente()));
    }

    [Fact(DisplayName = "Deve Concluir com Sucesso")]
    public async void Deve_Concluir_Com_Sucesso()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        await MovimentoFixture.SetMovimentos(contaCorrente);
        IniciarMocks(contaCorrente);

        var handler = new SaldoContaCorrenteQueryHandler(_contaCorrenteRepository, _movimentoRepository);

        var command = new SaldoContaCorrenteQuery(idContaCorrente: contaCorrente.IdContaCorrente);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(347.5, result.ValorSaldoAtual);


        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), true);

        await _movimentoRepository.DidNotReceive()
            .BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>());
    }

    [Fact(DisplayName = "Deve Gerar Erro Conta Não Cadastrada")]
    public async void Deve_Gerar_Erro_Conta_Nao_Cadastrada()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        await MovimentoFixture.SetMovimentos(contaCorrente);

        var contaCorrenteRepositoryMock = Substitute.For<IContaCorrenteRepository>();
        contaCorrenteRepositoryMock.BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>())
                .Returns(Task.FromResult<ContaCorrente>(null));

        var handler = new SaldoContaCorrenteQueryHandler(contaCorrenteRepositoryMock, _movimentoRepository);
        var command = new SaldoContaCorrenteQuery(idContaCorrente: contaCorrente.IdContaCorrente);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InvalidAccountException>(async () => await fnHandler());
        Assert.Equal("Apenas contas correntes cadastradas podem consultar o saldo", exception.Message);

        await contaCorrenteRepositoryMock.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive()
            .BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>());
    }
    [Fact(DisplayName = "Deve Gerar Erro Conta Invalida")]
    public async void Deve_Gerar_Erro_Conta_Invalida()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterInativas()).First();
        await MovimentoFixture.SetMovimentos(contaCorrente);

        IniciarMocks(contaCorrente);

        var handler = new SaldoContaCorrenteQueryHandler(_contaCorrenteRepository, _movimentoRepository);
        var command = new SaldoContaCorrenteQuery(idContaCorrente: contaCorrente.IdContaCorrente);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InactiveAccountException>(async () => await fnHandler());
        Assert.Equal("Apenas contas correntes ativas podem consultar o saldo", exception.Message);

        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive()
            .BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>());
    }

    [Fact(DisplayName = "Deve Concluir com Sucesso - SQL View")]
    public async void Deve_Concluir_Com_Sucesso_SQL_View()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        await MovimentoFixture.SetMovimentos(contaCorrente: contaCorrente, exemplo1: false);
        IniciarMocks(contaCorrente);

        var handler = new SaldoContaCorrenteQueryHandler(_contaCorrenteRepository, _movimentoRepository);
        var command = new SaldoContaCorrenteViewSQLQuery(idContaCorrente: contaCorrente.IdContaCorrente);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(-1.5, result.ValorSaldoAtual);


        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), false);

        await _movimentoRepository.Received(1)
            .BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>());
    }

    [Fact(DisplayName = "Deve Gerar Erro Conta Não Cadastrada - SQL View")]
    public async void Deve_Gerar_Erro_Conta_Nao_Cadastrada_SQL_View()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterPrimeiro());
        await MovimentoFixture.SetMovimentos(contaCorrente);

        var contaCorrenteRepositoryMock = Substitute.For<IContaCorrenteRepository>();
        contaCorrenteRepositoryMock.BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>())
                .Returns(Task.FromResult<ContaCorrente>(null));

        var handler = new SaldoContaCorrenteQueryHandler(contaCorrenteRepositoryMock, _movimentoRepository);
        var command = new SaldoContaCorrenteViewSQLQuery(idContaCorrente: contaCorrente.IdContaCorrente);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InvalidAccountException>(async () => await fnHandler());
        Assert.Equal("Apenas contas correntes cadastradas podem consultar o saldo", exception.Message);

        await contaCorrenteRepositoryMock.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive()
            .BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>());
    }
    [Fact(DisplayName = "Deve Gerar Erro Conta Invalida - SQL View")]
    public async void Deve_Gerar_Erro_Conta_Invalida_SQL_View()
    {
        //Arrange
        var contaCorrente = (await ContaCorrenteFixture.ObterInativas()).First();
        await MovimentoFixture.SetMovimentos(contaCorrente);

        IniciarMocks(contaCorrente);

        var handler = new SaldoContaCorrenteQueryHandler(_contaCorrenteRepository, _movimentoRepository);
        var command = new SaldoContaCorrenteViewSQLQuery(idContaCorrente: contaCorrente.IdContaCorrente);

        // Act
        var fnHandler = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        var exception = await Assert.ThrowsAsync<InactiveAccountException>(async () => await fnHandler());
        Assert.Equal("Apenas contas correntes ativas podem consultar o saldo", exception.Message);

        await _contaCorrenteRepository.Received(1)
              .BuscarPorIdContaCorrente(Arg.Any<Guid>(), Arg.Any<bool>());

        await _movimentoRepository.DidNotReceive()
            .BuscarSaldoPorIdContaCorrente(Arg.Any<Guid>());
    }
}
