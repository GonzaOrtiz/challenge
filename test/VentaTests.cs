using challenge.src.Api.Dtos;
using challenge.src.Application.Ventas;
using challenge.src.Application.Modelos;
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
        private readonly IModeloBusiness _modeloBusiness;
        private readonly VentaBusiness _ventaBusiness;
        private readonly Guid _centroNorteId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private readonly Guid _centroSurId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private readonly Guid _sportModeloId = Guid.Parse("00000000-0000-0000-0000-000000000004");
        private readonly Guid _sedanModeloId = Guid.Parse("00000000-0000-0000-0000-000000000001");

        public VentaTests()
        {
            _modeloBusiness = new ModeloBusiness(_modeloRepository);
            _ventaBusiness = new VentaBusiness(_ventaRepository, _modeloBusiness);
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
            var sedan = modelos.First(m => m.Id == _sedanModeloId);
            var sport = modelos.First(m => m.Id == _sportModeloId);

            var req = new VentaRequestDto
            {
                CentroDistribucionId = _centroNorteId,
                Fecha = DateTime.UtcNow,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = _sedanModeloId, Cantidad = 2 },
                    new VentaDetalleDto { ModeloId = _sportModeloId, Cantidad = 1 }
                }
            };

            // Expected calculations
            decimal expectedSedan = sedan.PrecioBase * 2;
            decimal expectedSport = sport.PrecioBase * (1 + ImpuestosConstantes.IMPUESTO_EXTRA_SPORT);
            var expectedTotal = expectedSedan + expectedSport;

            // Act
            var success = _ventaBusiness.InsertarVenta(req);
            var volumenTotal = _ventaBusiness.ObtenerVolumenTotal();
            var ventas = _ventaRepository.GetAll();
            var ventaInsertada = ventas.FirstOrDefault(v => v.CentroDistribucionId == _centroNorteId);

            // Assert
            Assert.True(success);
            Assert.NotNull(ventaInsertada);
            Assert.Equal(_centroNorteId, ventaInsertada.CentroDistribucionId);
            Assert.Equal(2, ventaInsertada.Detalles.Count());
            Assert.Equal(decimal.Round(expectedTotal, 2), decimal.Round(volumenTotal, 2));
        }

    [Fact]
    public void InsertarVenta_ConDatosInvalidos_DeberiaFallar()
    {
        // Arrange
        var reqInvalido = new VentaRequestDto
        {
            CentroDistribucionId = Guid.NewGuid(), // Centro que no existe
            Fecha = DateTime.UtcNow,
            Detalles = new List<VentaDetalleDto>
            {
                new VentaDetalleDto { ModeloId = Guid.NewGuid(), Cantidad = 1 } // Modelo que no existe
            }
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => _ventaBusiness.InsertarVenta(reqInvalido));
        Assert.Contains("no existe", exception.Message);
    }

    [Fact]
    public void ObtenerVolumenPorCentro_DeberiaRetornarSumaCorrectaPorCentro()
        {
            // Arrange
            ClearVentas();
            var modelos = _modeloRepository.GetAll().ToList();
            var sedan = modelos.First(m => m.Id == _sedanModeloId);

            var req1 = new VentaRequestDto
            {
                CentroDistribucionId = _centroNorteId,
                Fecha = DateTime.UtcNow,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = _sedanModeloId, Cantidad = 3 }
                }
            };

            var req2 = new VentaRequestDto
            {
                CentroDistribucionId = _centroSurId,
                Fecha = DateTime.UtcNow,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = _sedanModeloId, Cantidad = 1 }
                }
            };

            _ventaBusiness.InsertarVenta(req1);
            _ventaBusiness.InsertarVenta(req2);

            var expectedCentroNorte = sedan.PrecioBase * 3;
            var expectedCentroSur = sedan.PrecioBase * 1;

            // Act
            var volumenNorte = _ventaBusiness.ObtenerVolumenPorCentro(_centroNorteId);
            var volumenSur = _ventaBusiness.ObtenerVolumenPorCentro(_centroSurId);

            // Assert
            Assert.Equal(expectedCentroNorte, volumenNorte);
            Assert.Equal(expectedCentroSur, volumenSur);
        }

    [Fact]
    public void ObtenerPorcentajeModelosPorCentro_DeberiaRetornarPorcentajesCorrectos()
        {
            // Arrange
            ClearVentas();
            var modelos = _modeloRepository.GetAll().ToList();
            
            // 3 sedan, 1 sport => total 4 units -> sedan 75%, sport 25%
            var req = new VentaRequestDto
            {
                CentroDistribucionId = _centroNorteId,
                Fecha = DateTime.UtcNow,
                Detalles = new List<VentaDetalleDto>
                {
                    new VentaDetalleDto { ModeloId = _sedanModeloId, Cantidad = 3 },
                    new VentaDetalleDto { ModeloId = _sportModeloId, Cantidad = 1 }
                }
            };

            _ventaBusiness.InsertarVenta(req);

            // Act
            var porcentajes = _ventaBusiness.ObtenerPorcentajeModelosPorCentro(_centroNorteId);

            // Assert
            Assert.NotNull(porcentajes);
            var porcentajesList = porcentajes.ToList();
            
            Assert.Equal(2, porcentajesList.Count);
            var sedanPorcentaje = porcentajesList.First(p => p.Key.Equals("Sedan"));
            var sportPorcentaje = porcentajesList.First(p => p.Key.Equals("Sport"));
            Assert.Equal(75.00m, sedanPorcentaje.Value);
            Assert.Equal(25.00m, sportPorcentaje.Value);
        }

    [Fact]
    public void ObtenerPorcentajeModelosPorCentro_SinVentas_RetornaListaVacia()
        {
            // Arrange
            ClearVentas();

            // Act
            var porcentajes = _ventaBusiness.ObtenerPorcentajeModelosPorCentro(_centroNorteId);

            // Assert
            Assert.NotNull(porcentajes);
            Assert.Empty(porcentajes);
        }

    [Fact]
    public void ObtenerVolumenPorCentro_CentroInexistente_DebeRetornarCero()
    {
        // Arrange
        var centroInexistente = Guid.NewGuid();

        // Act
        var volumen = _ventaBusiness.ObtenerVolumenPorCentro(centroInexistente);

        // Assert
        Assert.Equal(0m, volumen);
    }
    }
}
