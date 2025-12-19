namespace Simple.App.Services.Navigation;

public interface INavigationService
{
    /// <summary>
    /// Navega para a rota especificada.
    /// </summary>
    /// <param name="route">A rota para a qual a navegação será realizada.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de navegação.</returns>
    Task GoToAsync(string route);

    /// <summary>
    /// Navega para a rota especificada com os parâmetros fornecidos.
    /// </summary>
    /// <param name="route">A rota para a qual a navegação será realizada.</param>
    /// <param name="parameters">Os parâmetros a serem passados para a rota durante a navegação.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona de navegação.</returns>
    Task GoToAsync(string route, Dictionary<string, object> parameters);
}