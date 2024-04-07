using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces;
public interface IIdempotenciaRepository
{
    public Task<Idempotencia> BuscarPorChaveIdempotencia(Guid chaveIdempotencia);
    public Task<Idempotencia> Salvar(Idempotencia idempotencia);
}
