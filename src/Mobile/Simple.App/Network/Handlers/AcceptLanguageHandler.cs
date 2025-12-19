using System.Globalization;
using System.Net.Http.Headers;

namespace Simple.App.Network.Handlers;

/// <summary>
/// Classe responsável por adicionar o cabeçalho "Accept-Language" a todas
/// as requisições HTTP realizadas por um cliente HTTP. Este cabeçalho reflete
/// a cultura atual da aplicação, fornecendo informações sobre o idioma preferido.
/// </summary>
/// <remarks>
/// O valor do cabeçalho "Accept-Language" é obtido a partir da propriedade
/// CultureInfo.CurrentCulture.Name, garantindo que o idioma utilizado seja
/// sincronizado com as configurações culturais do ambiente de execução.
/// </remarks>
public sealed class AcceptLanguageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(CultureInfo.CurrentCulture.Name));
        return base.SendAsync(request, cancellationToken);
    }
}