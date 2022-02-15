var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

string? connectionString = configuration.GetConnectionString("DbConnection");

IServiceCollection services = builder.Services;

services.AddIdentityServer()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();
app.MapGet("/", () => "Hello World!");

app.Run();
