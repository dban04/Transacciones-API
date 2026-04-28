namespace Transacciones.API.DTOs;

public record class RetiroRequest(int CuentaId, decimal Monto, string Descripcion);