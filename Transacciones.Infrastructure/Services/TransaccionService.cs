using Microsoft.EntityFrameworkCore;
using Transacciones.Core.Common;
using Transacciones.Core.DTOs;
using Transacciones.Core.Entities;
using Transacciones.Core.Interfaces;
using Transacciones.Infrastructure.Data;

namespace Transacciones.Infrastructure.Services;

public class TransaccionService : ITransaccionService
{
    private readonly ApplicationDbContext _context;
    
    public TransaccionService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Transaccion>> ObtenerHistorial(int cuentaId)
    {
        return await _context.Transacciones
            .Where(t => t.CuentaId == cuentaId)
            .OrderByDescending(t => t.FechaTransaccion)
            .ToListAsync();
    }

    public async Task<Result<TransaccionResponseDTO>> RealizarAbono(int cuentaId, decimal monto, string descripcion)
    {
        using var dbTransaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var cuenta = await _context.Cuentas.FindAsync(cuentaId);

            if (cuenta == null) 
                return Result<TransaccionResponseDTO>.Failure("Cuenta no encontrada", 404);

            decimal saldoAnterior = cuenta.Saldo;

            cuenta.RegistrarAbono(monto);

            var transaccion = new Transaccion(
                cuentaId, 
                "ABONO", 
                monto, 
                saldoAnterior, 
                cuenta.Saldo, 
                descripcion
            );

            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            return Result<TransaccionResponseDTO>.Success(MapToDTO(transaccion));
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync();
            return Result<TransaccionResponseDTO>.Failure("Error interno al procesar el abono", 500);
        }
    }

    public async Task<Result<TransaccionResponseDTO>> RealizarRetiro(int cuentaId, decimal monto, string descripcion)
    {
        using var dbTransaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var cuenta = await _context.Cuentas.FindAsync(cuentaId);

            if (cuenta == null) return Result<TransaccionResponseDTO>.Failure("Cuenta no encontrada", 404);
            if (!cuenta.Activa) return Result<TransaccionResponseDTO>.Failure("La cuenta no está activa", 400);

            decimal saldoAnterior = cuenta.Saldo;
            try 
            {
                cuenta.RegistrarRetiro(monto);
            }
            catch (InvalidOperationException ex)
            {
                return Result<TransaccionResponseDTO>.Failure(ex.Message, 400);
            }

            var transaccion = new Transaccion(
                cuentaId, 
                "RETIRO", 
                monto, 
                saldoAnterior, 
                cuenta.Saldo, 
                descripcion
            );

            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            await dbTransaction.CommitAsync();

            return Result<TransaccionResponseDTO>.Success(MapToDTO(transaccion));
        }
        catch (Exception)
        {
            await dbTransaction.RollbackAsync();
            return Result<TransaccionResponseDTO>.Failure("Error interno al procesar el retiro", 500);
        }
    }
    private TransaccionResponseDTO MapToDTO(Transaccion t)
        {
            return new TransaccionResponseDTO
            {
                Id = t.Id,
                TipoTransaccion = t.TipoTransaccion,
                Monto = t.Monto,
                SaldoAnterior = t.SaldoAnterior,
                SaldoNuevo = t.SaldoNuevo,
                FechaTransaccion = t.FechaTransaccion,
                Descripcion = t.Descripcion
            };
        }
}
