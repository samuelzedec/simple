using Simple.App.ViewModels.Auth;

namespace Simple.App.Views.Auth.Login;

public partial class LoginPage
{
    public LoginPage(LoginViewModel viewModel) 
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}