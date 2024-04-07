using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.Interfaces;
public interface IMovimentoRepository
{
    public Task<IEnumerable<Movimento>> BuscarTodosPorIdContaCorrente(Guid idContaCorrente);
    public Task<double> BuscarSaldoPorIdContaCorrente(Guid idContaCorrente);
    public Task<Movimento> Salvar(Movimento movimento);
}
