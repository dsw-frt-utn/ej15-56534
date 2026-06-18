using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data.Interfaces;
using Dsw2026Ej15.Data.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.MapHealthChecks("/health-check");

app.Run();