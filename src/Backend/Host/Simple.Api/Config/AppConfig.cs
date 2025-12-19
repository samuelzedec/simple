using Identity.Api;
using Scalar.AspNetCore;

namespace Simple.Api.Config;

public static class AppConfig
{
    extension(WebApplication app)
    {
        public void UseConfig()
        {
            app.UseHttpsRedirection();
            app.UseDocumentation();
            app.UseEndpointModules();
        }

        private void UseDocumentation()
        {
            if (!app.Environment.IsDevelopment())
                return;

            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        private void UseEndpointModules()
        {
            app.UseAddIdentityEndpoints();
        }
    }
}