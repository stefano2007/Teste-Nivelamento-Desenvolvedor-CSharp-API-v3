using Dapper;
using Questao5.Domain.Enumerators;
using System.Data;

namespace Questao5.Infrastructure.Database.Repository;

//Classe para configura tipo customizados devido as limitações do SQLite
//referencia: https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/dapper-limitations
public abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T>
{
    // Parameters are converted by Microsoft.Data.Sqlite
    public override void SetValue(IDbDataParameter parameter, T value)
        => parameter.Value = value;
}

public class DateTimeHandler : SqliteTypeHandler<DateTime>
{
    public override DateTime Parse(object value)
        => DateTime.Parse((string)value);

    public override void SetValue(IDbDataParameter parameter, DateTime value)
        => parameter.Value = value.ToString("dd/MM/yyyy HH:mm:ss");
}

public class GuidHandler : SqliteTypeHandler<Guid>
{
    public override Guid Parse(object value)
        => Guid.Parse((string)value);
}

public class TipoMovimentoTypeHandler : SqliteTypeHandler<TipoMovimento>
{
    public override TipoMovimento Parse(object value){
        string stringValue = (string)value;
        if (Enum.TryParse(stringValue, out TipoMovimento result))
            return result;

        throw new ArgumentException($"Requested value '{stringValue}' was not found.");
    }
}