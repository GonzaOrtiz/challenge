using System;
using AutoMapper;
using challenge.src.Api.Dtos;
using challenge.src.Domain.Constants;
using challenge.src.Domain.Entities;
using challenge.src.Infrastructure.Modelo;
using challenge.src.Infrastructure.Venta;

namespace challenge.src.Application.Venta
{
    public class VentaBusiness : IVentaBusiness
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IModeloRepository _modeloRepository;

        public VentaBusiness(
            IVentaRepository ventaRepository,
            IModeloRepository modeloRepository)
        {
            _ventaRepository = ventaRepository;
            _modeloRepository = modeloRepository;
        }

        public bool InsertarVenta(VentaRequestDto req)
        {
            // Validar que los modelos existan y obtener sus precios
            var venta = new Domain.Entities.Venta
            {
                CentroDistribucionId = req.CentroDistribucionId,
                Fecha = req.Fecha,
                Detalles = new List<VentaDetalle>()
            };

            foreach (var detalle in req.Detalles)
            {
                // Obtener el modelo del repositorio
                var modelo = _modeloRepository.GetById(detalle.ModeloId);
                if (modelo is null)
                {
                    throw new ArgumentException($"El modelo con ID {detalle.ModeloId} no existe");
                }

                // Calcular impuesto extra según el tipo de modelo
                decimal impuestoExtra = CalcularImpuestoExtra(modelo, detalle.Cantidad);

                var ventaDetalle = new VentaDetalle
                {
                    ModeloId = detalle.ModeloId,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitarioLista = modelo.PrecioBase,
                    ImpuestoExtraUnitario = impuestoExtra
                };

                venta.Detalles.Add(ventaDetalle);
            }

            return _ventaRepository.insert(venta);
        }

        private decimal CalcularImpuestoExtra(Modelo modelo, int cantidad)
        {
            if (modelo.Tipo == Domain.Enums.TipoModelo.Sport)
            {
                return modelo.PrecioBase * ImpuestosConstantes.IMPUESTO_EXTRA_SPORT;
            }

            return 0;
        }

        public decimal ObtenerVolumenTotal()
        {
            return _ventaRepository.GetVolumenTotal();
        }

        public IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null)
        {
            return _ventaRepository.GetPorcentajeModelosPorCentro(centroId);
        }

        public decimal ObtenerVolumenPorCentro(Guid centroId)
        {
            return _ventaRepository.GetVolumenPorCentro(centroId);
        }

    }
}
