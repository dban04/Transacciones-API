using Moq;
using Transacciones.Core.Interfaces;
using Transacciones.API.Controllers;
using Transacciones.API.DTOs;
using Transacciones.Core.DTOs;
using Transacciones.Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace Transacciones.Tests.Controllers;

public class TransaccionesControllerTests
{
    private readonly Mock<ITransaccionService> _serviceMock;
    private readonly TransaccionesController _controller;

    public TransaccionesControllerTests()
    {
        _serviceMock = new Mock<ITransaccionService>();
        _controller = new TransaccionesController(_serviceMock.Object);
    }
    [Fact]
    public async Task RealizarRetiro_CuandoEsExitoso_RetornaOk()
    {
        // Arrange
        var request = new RetiroRequest(1, 100, "Retiro cajero");
        var responseDto = new TransaccionResponseDTO { Id = 1, Monto = 100, SaldoNuevo = 900 };

        // Simulamos que el servicio devuelve éxito
        _serviceMock.Setup(s => s.RealizarRetiro(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<string>()))
                    .ReturnsAsync(Result<TransaccionResponseDTO>.Success(responseDto));

        // Act
        var result = await _controller.RealizarRetiro(request);
        
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
    [Fact]
    public async Task RealizarRetiro_CuandoFalla_RetornaStatusCodeCorrespondiente()
    {
        // Arrange
        var request = new RetiroRequest(1, 5000, "Retiro excesivo");

        // Simulamos que el servicio devuelve un fallo de saldo insuficiente (400)
        _serviceMock.Setup(s => s.RealizarRetiro(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<string>()))
                    .ReturnsAsync(Result<TransaccionResponseDTO>.Failure("Saldo insuficiente", 400));

        // Act
        var result = await _controller.RealizarRetiro(request);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, objectResult.StatusCode);
    }

}
