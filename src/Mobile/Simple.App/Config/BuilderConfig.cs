using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Simple.App.Services.Navigation;
using Simple.App.ViewModels.Auth;

namespace Simple.App.Config;

public static class BuilderConfig
{
    extension(MauiAppBuilder builder)
    {
        public MauiAppBuilder ConfigureAppSettings()
        {
            using var fileStream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("Simple.App.Resources.appsettings.json")!;

            var config = new ConfigurationBuilder()
                .AddJsonStream(fileStream)
                .Build();

            builder.Configuration.AddConfiguration(config);
            return builder;
        }

        /// <summary>
        /// Configura o sistema de registro de logs da aplicação.
        /// </summary>
        /// <returns>Uma instância de <see cref="MauiAppBuilder"/> com o sistema de logging configurado.</returns>
        public MauiAppBuilder ConfigureLogging()
        {
#if DEBUG
            builder.Logging.SetMinimumLevel(LogLevel.Debug);
            builder.Logging.AddDebug();
#else
            builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endif
            return builder;
        }

        /// <summary>
        /// Configura a integração do Sentry para monitoramento e rastreamento de erros na aplicação.
        /// </summary>
        /// <returns>Uma instância de <see cref="MauiAppBuilder"/> com o Sentry configurado.</returns>
        public MauiAppBuilder ConfigureSentry()
        {
            var sentryDsn = builder.Configuration["Sentry:Dsn"];
            builder
                .UseSentry(options =>
                {
                    options.Dsn = sentryDsn;
#if DEBUG
                    options.Debug = true;
                    options.TracesSampleRate = 1.0;
#else
                    options.TracesSampleRate = 0.2;
#endif
                });

            return builder;
        }

        /// <summary>
        /// Configura os serviços utilizados pela aplicação, registrando-os no contêiner de injeção de dependências.
        /// </summary>
        /// <returns>Uma instância de <see cref="MauiAppBuilder"/> com os serviços configurados.</returns>
        public MauiAppBuilder ConfigureServices()
        {
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            return builder;
        }

        public MauiAppBuilder ConfigureRoutes()
        {
            builder.Services.AddTransient<LoginViewModel>();

            return builder;
        }
    }
}