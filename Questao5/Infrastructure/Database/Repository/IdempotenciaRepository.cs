using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.Repository;
public class IdempotenciaRepository : IIdempotenciaRepository
{
    DatabaseConfig _databaseConfig;
    public IdempotenciaRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }
    public async Task<Idempotencia> BuscarPorChaveIdempotencia(Guid chaveIdempotencia)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);

        var sql = "SELECT chave_idempotencia, requisicao, resultado FROM idempotencia where chave_idempotencia = @chaveIdempotencia";
        var param = new { chaveIdempotencia };
        return await connection.QuerySingleOrDefaultAsync<Idempotencia>(sql, param);
    }
    public async Task<Idempotencia> Salvar(Idempotencia idempotencia)
    {
        using var connection = new SqliteConnection(_databaseConfig.Name);

        var sql = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@chave_idempotencia, @requisicao, @resultado)";
        var param = new { chave_idempotencia = idempotencia.Chave_Idempotencia, 
                          requisicao = idempotencia.Requisicao, 
                          resultado = idempotencia.Resultado 
        };
        await connection.ExecuteAsync(sql, param);
        return idempotencia;
    }
}
