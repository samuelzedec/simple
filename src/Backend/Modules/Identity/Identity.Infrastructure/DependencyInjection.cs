using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

/// <summary>
/// Fornece métodos para configurar e gerenciar a injeção de dependência
/// na camada de infraestrutura do módulo de Identity.
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public void AddInfrastructure(IConfiguration configuration)
        {
            services.AddPersistence(configuration);
        }

        private void AddPersistence(IConfiguration configuration)
        {
            var isDevelopment = configuration["ASPNETCORE_ENVIRONMENT"] == "Development";
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<IdentityDatabaseContext>(options => options
                .UseNpgsql(connectionString, b => b
                    .MigrationsAssembly(typeof(IdentityDatabaseContext).Assembly.FullName)
                    .MigrationsHistoryTable("__IdentityMigrationsHistory", "Identity"))
                .EnableServiceProviderCaching()
                .EnableDetailedErrors(isDevelopment)
                .EnableSensitiveDataLogging(isDevelopment)
            );
        }
    }
}