using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Api.Common;

/// <summary>
/// Fornece métodos de extensão para configurar e mapear endpoints de APIs.
/// </summary>
public static class EndpointExtensions
{
    extension(IEndpointRouteBuilder app)
    {
        /// <summary>
        /// Mapeia um endpoint que implementa a interface <see cref="IEndpoint"/>
        /// para o pipeline de roteamento da aplicação.
        /// </summary>
        /// <typeparam name="TEndpoint">
        /// Tipo do endpoint que implementa <see cref="IEndpoint"/>.
        /// </typeparam>
        /// <returns>
        /// A instância de <see cref="IEndpointRouteBuilder"/> para permitir encadeamento.
        /// </returns>
        public IEndpointRouteBuilder MapEndpoint<TEndpoint>()
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}