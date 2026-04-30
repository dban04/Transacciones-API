using Microsoft.AspNetCore.Mvc;
using Transacciones.API.DTOs;
using Transacciones.Core.Entities;
using Transacciones.Infrastructure.Data;

namespace Transacciones.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;
        [HttpPost]
        public async Task<IActionResult> CrearCuenta([FromBody] CrearCuentaRequest request)
        {
            var nuevaCuenta = new Cuenta(request.NumeroCuenta, request.SaldoInicial, request.Titular);
            
            _context.Cuentas.Add(nuevaCuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuenta), new { id = nuevaCuenta.Id }, nuevaCuenta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCuenta(int id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null) return NotFound();
            return Ok(cuenta);
        }
    }
}
