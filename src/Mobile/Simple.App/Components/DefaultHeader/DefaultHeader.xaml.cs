using CommunityToolkit.Mvvm.Input;
using Simple.App.Constants;

namespace Simple.App.Components.DefaultHeader;

public partial class DefaultHeader
{
    public static readonly BindableProperty TitleProperty = BindableProperty
        .Create(nameof(Title), typeof(string), typeof(DefaultHeader), string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public DefaultHeader()
        => InitializeComponent();

    [RelayCommand]
    private async Task GoToBackAsync()
        => await Shell.Current.GoToAsync(RoutePages.BackRoute);
}