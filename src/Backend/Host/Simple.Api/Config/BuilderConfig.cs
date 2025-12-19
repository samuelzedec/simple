using Identity.Api;
using Microsoft.OpenApi;
using Simple.ServiceDefaults;

namespace Simple.Api.Config;

public static class BuilderConfig
{
    extension(WebApplicationBuilder builder)
    {
        public void AddConfig()
        {
            builder.AddServiceDefaults();
            builder.AddDocumentation();
            builder.AddModulesServices();
        }

        private void AddDocumentation()
        {
            builder.Services.AddOpenApi(options => options
                .AddDocumentTransformer((document, _, _) =>
                {
                    document.Info = new OpenApiInfo()
                    {
                        Title = "Simple Api",
                        Version = "v1"
                    };
                    return Task.CompletedTask;
                })
            );
        }

        private void AddModulesServices()
        {
            builder.AddIdentityModule(builder.Configuration);
        }
    }
}