using Microsoft.EntityFrameworkCore;
using Transacciones.Infrastructure.Data;
using Transacciones.Infrastructure.Services;
using Transacciones.Core.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Transacciones.Tests.Services;

public class TransaccionServiceTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        return new ApplicationDbContext(options);
    }
    [Fact]
    public async Task RealizarRetiro_ConSaldoInsuficiente_DeberiaRetornarError()
    {
        // 1. ARRANGE (Preparar el escenario)
        var context = GetDbContext();
        var service = new TransaccionService(context);

        // Creamos una cuenta con solo 100 pesos
        var cuenta = new Cuenta("CTA-001", 100, "Juan Perez");
        context.Cuentas.Add(cuenta);
        await context.SaveChangesAsync();
        // 2. ACT (Ejecutar la acción)
        // Intentamos retirar 500 pesos
        var resultado = await service.RealizarRetiro(cuenta.Id, 500, "Retiro excesivo");
        // 3. ASSERT (Verificar que falló correctamente)
        Assert.False(resultado.IsSuccess);
        Assert.Equal(400, resultado.StatusCode);
        Assert.Equal("No tienes suficiente Saldo", resultado.Error);
    }
    [Fact]
    public async Task RealizarAbono_DeberiaActualizarSaldoCorrectamente()
    {
        // Arrange
        var context = GetDbContext();
        var service = new TransaccionService(context);
        var cuenta = new Cuenta("CTA-002", 1000, "Maria Lopez");
        context.Cuentas.Add(cuenta);
        await context.SaveChangesAsync();

        // Act
        var resultado = await service.RealizarAbono(cuenta.Id, 200, "Deposito de regalo");

        // Assert
        Assert.True(resultado.IsSuccess);
        Assert.Equal(1200, resultado.Value.SaldoNuevo);
        
        // Verificar que se guardó en la BD real de memoria
        var cuentaDb = await context.Cuentas.FindAsync(cuenta.Id);
        Assert.Equal(1200, cuentaDb.Saldo);
    }
}
