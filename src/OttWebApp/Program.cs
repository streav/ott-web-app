using OttWebApp.Core.Entity;
using OttWebApp.Infrastructure;
using OttWebApp.Infrastructure.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("db");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string is not configured.");
}

builder.Services.AddEntityFramework(connectionString);

builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var streavConf = builder.Configuration.GetSection("Streav");

var apiBaseUrl = streavConf.GetValue<string>("ApiBaseUrl");
var clientId = streavConf.GetValue<string>("ClientId");
var clientSecret = streavConf.GetValue<string>("ClientSecret");

if (string.IsNullOrWhiteSpace(apiBaseUrl))
{
    throw new InvalidOperationException("Streav API base url is not configured.");
}

if (string.IsNullOrWhiteSpace(clientId))
{
    throw new InvalidOperationException("Streav API client ID is not configured.");
}

if (string.IsNullOrWhiteSpace(clientSecret))
{
    throw new InvalidOperationException("Streav API client secret is not configured.");
}

builder.Services.AddStreavHttpClient(apiBaseUrl, clientId, clientSecret);

var app = builder.Build();

// migrate database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapGroup("/api/auth").MapIdentityApi<User>();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();