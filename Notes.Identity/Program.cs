var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

string? connectionString = configuration.GetSection("DbConnection").Value;

IServiceCollection services = builder.Services;

services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Notes.Identity.Cookie";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddDeveloperSigningCredential();

services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, "An error occurred while application initialization.");
    }
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});

app.UseRouting();

app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
