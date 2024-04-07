using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Questao5.Infrastructure.Configurations;
public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Conta Corrente",
                Version = "v1",
                Description = "Serviços de Movimentação de uma conta corrente e Saldo da conta corrente.",
            });

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
            var commentsFile = Path.Combine(baseDirectory, commentsFileName);
            options.IncludeXmlComments(commentsFile);
        });

        return services;
    }
    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Conta Corrente V1");
        });

        return app;
    }
}
