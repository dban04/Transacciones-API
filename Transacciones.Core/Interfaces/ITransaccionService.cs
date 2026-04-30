using Transacciones.Core.Common;
using Transacciones.Core.DTOs;
using Transacciones.Core.Entities;

namespace Transacciones.Core.Interfaces;

public interface ITransaccionService
{
    Task<Result<TransaccionResponseDTO>> RealizarAbono(int cuentaId, decimal monto, string descripcion);
    Task<Result<TransaccionResponseDTO>> RealizarRetiro(int cuentaId, decimal monto, string descripcion);
    Task<IEnumerable<Transaccion>> ObtenerHistorial(int cuentaId);
}
