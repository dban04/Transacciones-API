namespace Transacciones.Core.Exceptions;

public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException() : base("Saldo Insuficiente") { }
    public SaldoInsuficienteException(string message) : base(message) { }
}
