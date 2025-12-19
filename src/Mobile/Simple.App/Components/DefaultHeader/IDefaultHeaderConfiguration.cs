namespace Simple.App.Components.DefaultHeader;

public interface IDefaultHeaderConfiguration
{
    /// <summary>
    /// Indica se o cabeçalho deve ser exibido.
    /// </summary>
    bool ShowHeader { get; }

    /// <summary>
    /// Define o título exibido no cabeçalho.
    /// </summary>
    string HeaderTitle { get; }
}