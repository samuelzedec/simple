using Microsoft.AspNetCore.Builder;

namespace Identity.Api.Endpoints;

public static class Endpoints
{
    extension(WebApplication app)
    {
        public void MapEndpoints()
        {
            var endpoints = app
                .MapGroup("");
            
        }
    }
}