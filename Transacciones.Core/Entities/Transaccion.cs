namespace Transacciones.Core.Entities;

public class Transaccion
{
    public int Id { get; set; }
    public int CuentaId { get; set; }
    public string TipoTransaccion { get; set; }
    public decimal Monto { get; set; }
    public DateTime FechaTransaccion { get; set; }
    public string Descripcion { get; set; }
    public decimal SaldoAnterior { get; set; }
    public decimal SaldoNuevo {get; set; }

    private Transaccion() { }

    public Transaccion(
            int cuentaId, 
            string tipo, 
            decimal monto, 
            decimal saldoAnterior,
            decimal saldoNuevo,
            string descripcion
        )
    {
        CuentaId = cuentaId;
        TipoTransaccion = tipo;
        Monto = monto;
        SaldoAnterior = saldoAnterior;
        SaldoNuevo = saldoNuevo;
        Descripcion = descripcion;
        FechaTransaccion = DateTime.Now;
    }
}
