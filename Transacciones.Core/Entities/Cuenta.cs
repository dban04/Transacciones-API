namespace Transacciones.Core.Entities;

public class Cuenta
{
    public int Id { get; set; }
    public string NumeroCuenta { get; set; }
    public decimal Saldo { get; set; }
    public string Titular { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool Activa { get; set; }

    private Cuenta() { }

    public Cuenta(string numeroCuenta, decimal saldoInicial, string titular)
    {
        if(saldoInicial < 0)
            throw new ArgumentException("El saldo inicial debe de ser mayor a 0");
        
        NumeroCuenta = numeroCuenta;
        Saldo = saldoInicial;
        Titular = titular;
        FechaCreacion = DateTime.Now;
        Activa = true;
    }

    public void RegistrarAbono(decimal monto)
    {
        ValidarEstadoActivo();
        if(monto <= 0) throw new ArgumentException("El Abono debe de ser mayor a 0");
        Saldo += monto;
    }
    public void RegistrarRetiro(decimal monto)
    {
        ValidarEstadoActivo();
        if(monto <= 0 ) throw new ArgumentException("El Monto debe de ser mayor a cero");
        if(Saldo < monto) throw new InvalidOperationException("No tienes suficiente Saldo");
        Saldo -= monto;
    }
    private void ValidarEstadoActivo()
    {
        if(!Activa)
            throw new InvalidOperationException("La Cuenta no esta Activa");
    }
}
