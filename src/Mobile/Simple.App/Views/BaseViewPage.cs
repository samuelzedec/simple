namespace Simple.App.Views;

public abstract class BaseViewPage : ContentPage
{
    public static readonly BindableProperty ShowHeaderProperty = 
        BindableProperty.Create(nameof(ShowHeader), typeof(bool), typeof(BaseViewPage), false);

    public static readonly BindableProperty HeaderTitleProperty = 
        BindableProperty.Create(nameof(HeaderTitle), typeof(string), typeof(BaseViewPage), string.Empty);

    /// <summary>
    /// Define se o cabeçalho da página deve ser exibido ou não.
    /// </summary>
    /// <remarks>
    /// Quando verdadeiro, o cabeçalho padrão será exibido no topo da página.
    /// Caso contrário, o cabeçalho será ocultado. A visibilidade do cabeçalho pode ser útil
    /// para páginas que não requerem um título ou navegação na parte superior.
    /// </remarks>
    public bool ShowHeader
    {
        get => (bool)GetValue(ShowHeaderProperty);
        set => SetValue(ShowHeaderProperty, value);
    }

    /// <summary>
    /// Representa o título do cabeçalho exibido na página.
    /// </summary>
    /// <remarks>
    /// Este valor é exibido no componente de cabeçalho padrão da interface de usuário.
    /// Ele pode ser utilizado para identificar o conteúdo ou propósito da página atual,
    /// permitindo uma experiência de navegação mais clara para o usuário.
    /// </remarks>
    public string HeaderTitle
    {
        get => (string)GetValue(HeaderTitleProperty);
        set => SetValue(HeaderTitleProperty, value);
    }
}