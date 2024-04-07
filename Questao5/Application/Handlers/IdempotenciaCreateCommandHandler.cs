using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;

namespace Questao5.Application.Handlers;
public class IdempotenciaCreateCommandHandler : IRequestHandler<IdempotenciaCreateCommand, IdempotenciaCreateResponse>
{
    private readonly IIdempotenciaRepository _idempotenciaRepository;

    public IdempotenciaCreateCommandHandler(IIdempotenciaRepository idempotenciaRepository)
    {
        _idempotenciaRepository = idempotenciaRepository;
    }

    public async  Task<IdempotenciaCreateResponse> Handle(IdempotenciaCreateCommand request, CancellationToken cancellationToken)
    {
        var idempotencia = new Idempotencia(request.ChaveIdempotencia, request.Requisicao, request.Resultado);

        return (IdempotenciaCreateResponse) await _idempotenciaRepository.Salvar(idempotencia);
    }
}
