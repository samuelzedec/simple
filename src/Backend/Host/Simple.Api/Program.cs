using Simple.Api.Config;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfig();

var app = builder.Build();
app.UseConfig();

await app.RunAsync();