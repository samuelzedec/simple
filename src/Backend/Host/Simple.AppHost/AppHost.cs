using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder
    .AddProject<Simple_Api>("simple-api")
    .WithExternalHttpEndpoints(); // Deixa acessivel fora do ambiente aspire

builder.Build().Run();
