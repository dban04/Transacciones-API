using Microsoft.AspNetCore.Mvc;
using Transacciones.API.DTOs;
using Transacciones.Core.Interfaces;

namespace Transacciones.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransaccionesController(ITransaccionService transaccionService) : ControllerBase
{
    private readonly ITransaccionService _transaccionService = transaccionService;
    [HttpPost("abono")]
    public async Task<IActionResult> RealizarAbono([FromBody] AbonoRequest request)
    {
        var result = await _transaccionService.RealizarAbono(request.CuentaId, request.Monto, request.Descripcion);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, new { message = result.Error });

        return Ok(result.Value);
    }
    [HttpPost("retiro")]
    public async Task<IActionResult> RealizarRetiro([FromBody] RetiroRequest request)
    {
        var result = await _transaccionService.RealizarRetiro(request.CuentaId, request.Monto, request.Descripcion);

        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, new { message = result.Error });
            
        return Ok(result.Value);
    }
    [HttpGet("cuenta/{id}")]
    public async Task<IActionResult> GetHistorial(int id)
    {
        var historial = await _transaccionService.ObtenerHistorial(id);
        return Ok(historial);
    }
}
