using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Api.Common;

/// <summary>
/// Define um contrato para endpoints da API,
/// padronizando o mapeamento de rotas.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Realiza o mapeamento das rotas do endpoint
    /// no pipeline de roteamento da aplicação.
    /// </summary>
    /// <param name="endpoints">
    /// Builder responsável pelo registro das rotas.
    /// </param>
    static abstract void Map(IEndpointRouteBuilder endpoints);
}