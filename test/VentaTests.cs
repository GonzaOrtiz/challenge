using challenge.src.Api.Dtos;
using challenge.src.Application.Ventas;
using challenge.src.Infrastructure.Modelos;
using challenge.src.Infrastructure.Ventas;
using challenge.src.Infrastructure;
using challenge.src.Domain.Enums;
using challenge.src.Domain.Constants;
using System.Linq;

namespace test
{
    public class VentaTests
    {
        private readonly IVentaRepository _ventaRepository = new VentaRepository();
        private readonly IModeloRepository _modeloRepository = new ModeloRepository();
        private readonly VentaBusiness _ventaBusiness;

        public VentaTests()
        {
            _ventaBusiness = new VentaBusiness(_ventaRepository, _modeloRepository);
            ClearVentas();
        }

        private void ClearVentas()
        {
            // Vaciar el ConcurrentBag InMemoryData.Ventas
            while (InMemoryData.Ventas.TryTake(out _)) { }
        }

    [Fact]
    public void InsertarVenta_DeberiaAgregarVenta_YCalcularTotal_ConImpuestoSport()
        {
            // Arrange
            ClearVentas();

            var modelos = _modeloRepository.GetAll().ToList();
            var sedan = modelos.First(m => m.Tipo == TipoModelo.Sedan);
            var sport = modelos.First(m => m.Tipo == TipoModelo.Sport);

            var centroId = Guid.NewGuid();

            var req = new VentaRequestDto
            {
                CentroDistribucionId = centroId,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = sedan.Id, Cantidad = 2 },
                    new VentaDetalleDto { ModeloId = sport.Id, Cantidad = 1 }
                }
            };

            // Expected calculations
            decimal expectedSedan = sedan.PrecioBase * 2;
            // Usar la constante para que la fórmula coincida con la lógica de negocio
            decimal expectedSport = sport.PrecioBase * (1 + ImpuestosConstantes.IMPUESTO_EXTRA_SPORT) * 1;
            var expectedTotal = expectedSedan + expectedSport;

            // Act
            var inserted = _ventaBusiness.InsertarVenta(req);
            var volumenTotal = _ventaBusiness.ObtenerVolumenTotal();

            // Assert
            Assert.True(inserted);
            Assert.Equal(decimal.Round(expectedTotal, 2), decimal.Round(volumenTotal, 2));
        }

    [Fact]
    public void ObtenerVolumenPorCentro_DeberiaRetornarSumaCorrectaPorCentro()
        {
            // Arrange
            ClearVentas();
            var modelos = _modeloRepository.GetAll().ToList();
            var suv = modelos.First(m => m.Tipo == TipoModelo.Suv);

            var centro1 = Guid.NewGuid();
            var centro2 = Guid.NewGuid();

            var req1 = new VentaRequestDto
            {
                CentroDistribucionId = centro1,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = suv.Id, Cantidad = 3 }
                }
            };

            var req2 = new VentaRequestDto
            {
                CentroDistribucionId = centro2,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = suv.Id, Cantidad = 1 }
                }
            };

            _ventaBusiness.InsertarVenta(req1);
            _ventaBusiness.InsertarVenta(req2);

            var expectedCentro1 = suv.PrecioBase * 3;
            var expectedCentro2 = suv.PrecioBase * 1;

            // Act
            var volumen1 = _ventaBusiness.ObtenerVolumenPorCentro(centro1);
            var volumen2 = _ventaBusiness.ObtenerVolumenPorCentro(centro2);

            // Assert
            Assert.Equal(expectedCentro1, volumen1);
            Assert.Equal(expectedCentro2, volumen2);
        }

    [Fact]
    public void ObtenerPorcentajeModelosPorCentro_DeberiaRetornarPorcentajesCorrectos()
        {
            // Arrange
            ClearVentas();
            var modelos = _modeloRepository.GetAll().ToList();
            var sedan = modelos.First(m => m.Tipo == TipoModelo.Sedan);
            var offroad = modelos.First(m => m.Tipo == TipoModelo.Offroad);

            var centro = Guid.NewGuid();

            // 3 sedan, 1 offroad => total 4 units -> sedan 75%, offroad 25%
            var req = new VentaRequestDto
            {
                CentroDistribucionId = centro,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = sedan.Id, Cantidad = 3 },
                    new VentaDetalleDto { ModeloId = offroad.Id, Cantidad = 1 }
                }
            };

            _ventaBusiness.InsertarVenta(req);

            // Act
            var porcentajes = _ventaBusiness.ObtenerPorcentajeModelosPorCentro(centro);

            // Assert
            Assert.NotNull(porcentajes);
            Assert.True(porcentajes.ContainsKey(sedan.Nombre));
            Assert.True(porcentajes.ContainsKey(offroad.Nombre));

            Assert.Equal(75.00m, porcentajes[sedan.Nombre]);
            Assert.Equal(25.00m, porcentajes[offroad.Nombre]);
        }

    [Fact]
    public void ObtenerPorcentajeModelosPorCentro_SinVentas_RetornaVacio()
        {
            // Arrange
            ClearVentas();
            var centro = Guid.NewGuid();

            // Act
            var porcentajes = _ventaBusiness.ObtenerPorcentajeModelosPorCentro(centro);

            // Assert
            Assert.NotNull(porcentajes);
            Assert.Empty(porcentajes);
        }
    }
}
