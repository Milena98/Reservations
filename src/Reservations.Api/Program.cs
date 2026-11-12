var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/ping", (IHostEnvironment env) =>
{
    return new PingResponse("OK", DateTimeOffset.UtcNow, env.EnvironmentName);
})
.WithName("Ping");

app.Run();


public record PingResponse(string Status, DateTimeOffset ServerTime, string Environment);