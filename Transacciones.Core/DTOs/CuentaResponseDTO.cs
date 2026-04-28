namespace Transacciones.Core.DTOs;

public record class CuentaResponseDTO
{
    public int Id { get; set; }
    public string NumeroCuenta { get; set; }
    public decimal Saldo { get; set; }
    public string Titular { get; set; }
    public bool Activa { get; set; }
}
