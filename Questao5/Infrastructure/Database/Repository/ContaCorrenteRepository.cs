using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository;
public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly DatabaseConfig _databaseConfig;
    private readonly IMovimentoRepository _movimentoRepository;
    public ContaCorrenteRepository(DatabaseConfig databaseConfig, IMovimentoRepository movimentoRepository)
    {
        _databaseConfig = databaseConfig;
        _movimentoRepository = movimentoRepository;
    }

    public async Task<ContaCorrente> BuscarPorIdContaCorrente(Guid idContaCorrente, bool incluirMovimentos)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);

        var sql = "SELECT idcontacorrente, numero, nome, ativo FROM contacorrente where idContaCorrente = @idContaCorrente";
        var param = new { idContaCorrente };
        var contaCorrente = await connection.QuerySingleOrDefaultAsync<ContaCorrente>(sql, param);

        if (contaCorrente is not null && incluirMovimentos)
        {
            var movimentos = await _movimentoRepository.BuscarTodosPorIdContaCorrente(contaCorrente.IdContaCorrente) 
                ?? new List<Movimento>();

            contaCorrente.SetMovimentos(movimentos);
        }
        return contaCorrente;
    }
}
