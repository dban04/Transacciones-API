namespace Transacciones.Core.DTOs;

public class TransaccionResponseDTO
{
    public int Id { get; set; }
    public string TipoTransaccion { get; set; }
    public decimal Monto { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoNuevo { get; set; }
    public DateTime FechaTransaccion { get; set; }
    public string Descripcion { get; set; }
}
