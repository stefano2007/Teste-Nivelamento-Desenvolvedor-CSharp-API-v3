using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Extensions;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers;
public class MovimentoCreateCommandHandler : IRequestHandler<MovimentoCreateCommand, MovimentoCreateResponse>
{
    private readonly IMovimentoRepository _movimentoRepository;
    private readonly IContaCorrenteRepository _contaCorrenteRepository;
    private readonly IIdempotenciaRepository _idempotenciaRepository;
    private readonly IMediator _mediator;

    public MovimentoCreateCommandHandler(IMovimentoRepository movimentoRepository, 
        IContaCorrenteRepository contaCorrenteRepository,
        IIdempotenciaRepository idempotenciaRepository,
        IMediator mediator)
    {
        _movimentoRepository = movimentoRepository;
        _contaCorrenteRepository = contaCorrenteRepository;
        _idempotenciaRepository = idempotenciaRepository;
        _mediator = mediator;
    }

    public async Task<MovimentoCreateResponse> Handle(MovimentoCreateCommand request, CancellationToken cancellationToken)
    {
        //verificar se existe um registro de idempotencia
        var idempotencia = await _idempotenciaRepository.BuscarPorChaveIdempotencia(request.IdRequisicao);
        if (idempotencia is not null && !string.IsNullOrEmpty(idempotencia.Resultado))
        {
            var movimentoResponse = idempotencia.Requisicao.ToObjetoByJson<MovimentoCreateCommand>();
            if (request.Equals(movimentoResponse))
            {
                return idempotencia.Resultado.ToObjetoByJson<MovimentoCreateResponse>();
            }
            throw new InvalidOperationException("Operação abortada, o Id Requisição já foi processado com corpo da requisão diferente");
        }

        // validações interna da entidade
        var motivo = new Movimento(idContaCorrente: request.IdContaCorrente, tipoMovimento: request.TipoMovimento, valor: request.Valor);

        var contaCorrente = await _contaCorrenteRepository.BuscarPorIdContaCorrente(request.IdContaCorrente, incluirMovimentos: false)
            ?? throw new InvalidAccountException("Apenas contas correntes cadastradas podem receber movimentação");

        if (!contaCorrente.Ativo)
        {
            throw new InactiveAccountException("Apenas contas correntes ativas podem receber movimentação");
        }

        var response =  (MovimentoCreateResponse)await _movimentoRepository.Salvar(motivo);
        await SalvarIdempotencia(request, response);
        return response;
    }

    public async Task SalvarIdempotencia(MovimentoCreateCommand request, MovimentoCreateResponse response)
    {        
        try
        {
            var idempotenciaCommand = new IdempotenciaCreateCommand(
                    chaveIdempotencia: request.IdRequisicao, 
                    requisicao: request.ToJson(), 
                    resultado: response.ToJson()
                );

            await _mediator.Send(idempotenciaCommand);
        }
        catch (Exception)
        {
            throw new Exception("Ocorreu um erro ao persistir a Idempotencia");
        }
    }

}
