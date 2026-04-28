namespace Transacciones.API.DTOs;

public record class CrearCuentaRequest(int CuentaId, decimal Monto, string Descripcion);