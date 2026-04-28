using Microsoft.EntityFrameworkCore;
using Transacciones.API.Middleware;
using Transacciones.Core.Interfaces;
using Transacciones.Infrastructure.Data;
using Transacciones.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICIOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de BD y Servicios
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITransaccionService, TransaccionService>();

var app = builder.Build();

// --- PIPELINE DE SOLICITUDES (EL ORDEN ES CRÍTICO) ---

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BancoSol API V1");
    options.RoutePrefix = "swagger";
});

// Middleware Global de Errores
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();