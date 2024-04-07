using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces;
public interface IContaCorrenteRepository
{
    public Task<ContaCorrente> BuscarPorIdContaCorrente(Guid idContaCorrente, bool incluirMovimentos);
}
