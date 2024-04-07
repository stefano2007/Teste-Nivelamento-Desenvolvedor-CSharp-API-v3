﻿using MediatR;
using Questao5.Application.Queries.Requests;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Validation;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers;
public class SaldoContaCorrenteQueryHandler 
        : IRequestHandler<SaldoContaCorrenteQuery, SaldoContaCorrenteResponse>,
          IRequestHandler<SaldoContaCorrenteViewSQLQuery, SaldoContaCorrenteResponse>
{
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IMovimentoRepository _movimentoRepository;
    public SaldoContaCorrenteQueryHandler(IContaCorrenteRepository contaCorrenteRepository, 
        IMovimentoRepository movimentoRepository)
    {
        _contaCorrenteRepository = contaCorrenteRepository;
        _movimentoRepository = movimentoRepository;
    }

    public async Task<SaldoContaCorrenteResponse> Handle(SaldoContaCorrenteQuery request, CancellationToken cancellationToken)
    {
        var contaCorrente = await _contaCorrenteRepository.BuscarPorIdContaCorrente(request.IdContaCorrente, incluirMovimentos: true)
                ?? throw new InvalidAccountException("Apenas contas correntes cadastradas podem consultar o saldo");

        if (!contaCorrente.Ativo)
        {
            throw new InactiveAccountException("Apenas contas correntes ativas podem consultar o saldo");
        }

        return SaldoContaCorrenteResponse.CriarResponse(
                    numero: contaCorrente.Numero, 
                    nomeTitular: contaCorrente.Nome, 
                    valorSaldoAtual: contaCorrente.SaldoContaCorrente()
                );
    }

    public async Task<SaldoContaCorrenteResponse> Handle(SaldoContaCorrenteViewSQLQuery request, CancellationToken cancellationToken)
    {
        var contaCorrente = await _contaCorrenteRepository.BuscarPorIdContaCorrente(request.IdContaCorrente, incluirMovimentos: false)
                ?? throw new InvalidAccountException("Apenas contas correntes cadastradas podem consultar o saldo");

        if (!contaCorrente.Ativo)
        {
            throw new InactiveAccountException("Apenas contas correntes ativas podem consultar o saldo");
        }

        return SaldoContaCorrenteResponse.CriarResponse(
                    numero: contaCorrente.Numero,
                    nomeTitular: contaCorrente.Nome,
                    valorSaldoAtual: await _movimentoRepository.BuscarSaldoPorIdContaCorrente(contaCorrente.IdContaCorrente)
                );
    }
}
