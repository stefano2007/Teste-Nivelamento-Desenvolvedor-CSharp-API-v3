using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Sqlite;
using System.Data;
using System.Drawing;

namespace Questao5.Infrastructure.Database.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig _databaseConfig;
        public MovimentoRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<double> BuscarSaldoPorIdContaCorrente(Guid idContaCorrente)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var sql = "SELECT Saldo FROM vwSaldoContaCorrente where idContaCorrente = @idContaCorrente";
            var param = new { idContaCorrente };
            return await connection.QuerySingleAsync<double>(sql, param);
        }

        public async Task<IEnumerable<Movimento>> BuscarTodosPorIdContaCorrente(Guid idContaCorrente)
        {           

            using var connection = new SqliteConnection(_databaseConfig.Name);

            var sql = "SELECT idmovimento, idcontacorrente, datamovimento, tipomovimento, valor FROM movimento where idContaCorrente = @idContaCorrente";
            var param = new { idContaCorrente };
            return await connection.QueryAsync<Movimento>(sql, param);           
        }

        public async Task<Movimento> Salvar(Movimento movimento)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var sql = "INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)"+
                      " VALUES (@idmovimento, @idcontacorrente, @datamovimento, @tipomovimento, @valor)";
            var param = new { 
                            idmovimento = movimento.IdMovimento, idcontacorrente = movimento.IdContaCorrente, 
                            datamovimento = movimento.DataMovimento.ToString("dd/MM/yyyy HH:mm:ss"), tipomovimento = movimento.TipoMovimento, 
                            valor = movimento.Valor
            };
            await connection.ExecuteAsync(sql, param);

            return movimento;
        }
    }   

}
