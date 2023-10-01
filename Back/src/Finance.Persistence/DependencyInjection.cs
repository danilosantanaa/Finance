using Finance.Persistence.Contextos;
using Finance.Persistence.Contratos;
using Finance.Persistence.Persistences;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.AddDbContext<FinanceContextDatabase>(context =>
            context.UseNpgsql(configuration.GetConnectionString("PostgresConnect"))
        );

        services.AddScoped<IGeneralPersistence, GeneralPersistence>();
        services.AddScoped<IBancoPersistence, BancoPersistence>();
        services.AddScoped<ICobrancaPersistence, CobrancaPersistence>();
        services.AddScoped<IReciboPersistence, ReciboPersistence>();
        services.AddScoped<ICategoriaPersistence, CategoriaPersistence>();
        services.AddScoped<ITransacionadorPersistence, TransacionadorPersistence>();
        services.AddScoped<IUserPersistence, UserPersistence>();

        return services;
    }
}
