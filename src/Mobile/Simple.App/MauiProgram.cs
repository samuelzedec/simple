using CommunityToolkit.Maui;
using Simple.App.Config;
using Simple.App.Constants;

namespace Simple.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureAppSettings()
            .ConfigureLogging()
            .ConfigureSentry()
            .ConfigureServices()
            .ConfigureRoutes()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Inter-Regular.ttf", FontFamily.MainFontRegular);
                fonts.AddFont("Inter-ExtraLight.ttf", FontFamily.MainFontExtraLight);
                fonts.AddFont("Inter-Bold.ttf", FontFamily.MainFontBold);
                fonts.AddFont("Inter-SemiBold.ttf", FontFamily.MainFontSemiBold);
            });

        return builder.Build();
    }
}