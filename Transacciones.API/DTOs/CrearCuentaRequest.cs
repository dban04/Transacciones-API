namespace Transacciones.API.DTOs;

public record class CrearCuentaRequest(string NumeroCuenta, decimal SaldoInicial, string Titular);