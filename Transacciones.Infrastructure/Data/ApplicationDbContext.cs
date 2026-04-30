using Microsoft.EntityFrameworkCore;
using Transacciones.Core.Entities;

namespace Transacciones.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroCuenta).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Titular).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Saldo).HasConversion<double>();

                entity.Property<byte[]>("RowVersion").IsRowVersion();
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoTransaccion).IsRequired();
                entity.Property(e => e.Monto).HasConversion<double>();
                entity.Property(e => e.SaldoAnterior).HasConversion<double>();
                entity.Property(e => e.SaldoNuevo).HasConversion<double>();
                
                entity.HasOne<Cuenta>()
                      .WithMany()
                      .HasForeignKey(e => e.CuentaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
}
