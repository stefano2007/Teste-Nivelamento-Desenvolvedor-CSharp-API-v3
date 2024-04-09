using Dapper;
using Questao5.Infrastructure.Database.Interfaces;
using Questao5.Infrastructure.Database.Repository;

namespace Questao5.Infrastructure.Configurations;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //repositories
        services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();
        services.AddScoped<IMovimentoRepository, MovimentoRepository>();
        services.AddScoped<IIdempotenciaRepository, IdempotenciaRepository>();
       
        return services;
    }
}
