namespace Transacciones.API.DTOs;

public record class AbonoRequest(int CuentaId, decimal Monto, string Descripcion);
