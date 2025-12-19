using Identity.Api.Endpoints;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Identity.Api;

/// <summary>
/// Classe responsável por registrar dependências e configurar o módulo de Identity.
/// </summary>
public static class DependencyInjection
{
    extension(WebApplication app)
    {
        /// <summary>
        /// Registra os endpoints do módulo de Identity na aplicação.
        /// </summary>
        public void UseAddIdentityEndpoints()
            => app.MapEndpoints();
    }

    extension(WebApplicationBuilder builder)
    {
        /// <summary>
        /// Registra o módulo de Identity e suas dependências no container de DI.
        /// </summary>
        /// <param name="configuration">Configurações da aplicação.</param>
        public void AddIdentityModule(IConfiguration configuration)
        {
            builder.Services.AddInfrastructure(configuration);
        }
    }
}