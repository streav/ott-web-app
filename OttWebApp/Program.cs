var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.Run();