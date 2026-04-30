using Transacciones.Core.Entities;

namespace Transacciones.Infrastructure.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Cuentas.Any()) return;

        var cuentas = new List<Cuenta>
        {
            new("1001-BS", 5000.00m, "Juana Campos"),
            new("2002-BS", 150.00m, "David Chavez"),
            new("3003-BS", 10.00m, "Jose Yujra"),
        };

            context.Cuentas.AddRange(cuentas);
            await context.SaveChangesAsync();
        var historial = new List<Transaccion>
        {
            new(cuentas[0].Id, "ABONO", 5000.00m, 0, 5000.00m, "Apertura de cuenta"),
            new(cuentas[1].Id, "ABONO", 150.50m, 0, 150.50m, "Deposito inicial")
        };

        context.Transacciones.AddRange(historial);
        await context.SaveChangesAsync();
    }
}
