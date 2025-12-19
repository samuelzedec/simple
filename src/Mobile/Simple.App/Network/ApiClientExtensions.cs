using Refit;
using Simple.App.Network.Handlers;

namespace Simple.App.Network;

public static class ApiClientExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adiciona um cliente de API à coleção de serviços, configurando-o com um
        /// manipulador HTTP e políticas de resiliência padrão.
        /// </summary>
        /// <typeparam name="T">A interface que define o cliente de API.</typeparam>
        /// <param name="services">A coleção de serviços onde o cliente será registrado.</param>
        /// <param name="apiUrl">A URL base da API que será consumida pelo cliente.</param>
        /// <returns>
        /// A coleção de serviços atualizada, incluindo o cliente de API configurado.
        /// </returns>
        public IServiceCollection AddApiClient<T>(string apiUrl)
            where T : class
        {
            services
                .AddRefitClient<T>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(apiUrl))
                .AddHttpMessageHandler<AcceptLanguageHandler>()
                .AddStandardResilienceHandler(options =>
                {
                    options.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(30);

                    options.Retry.MaxRetryAttempts = 3;
                    options.Retry.Delay = TimeSpan.FromSeconds(2);

                    options.CircuitBreaker.FailureRatio = 0.5d;
                    options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(20);
                    options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(10);
                });

            return services;
        }
    }
}