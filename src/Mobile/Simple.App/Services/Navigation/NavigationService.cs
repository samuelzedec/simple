namespace Simple.App.Services.Navigation;

public sealed class NavigationService : INavigationService
{
    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, Dictionary<string, object> parameters)
        => await Shell.Current.GoToAsync(route, parameters);
}