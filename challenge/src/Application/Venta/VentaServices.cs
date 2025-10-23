

using challenge.src.Api.Dtos;
using challenge.src.Domain.Entities;
using challenge.src.Infrastructure.Venta;

namespace challenge.src.Application.Venta
{
    public class VentaServices : IVentaServices
    {
        private readonly IVentaRepository _ventaRepository;
        public VentaServices(IVentaRepository ventaRepository)
        {
            _ventaRepository = ventaRepository;
        }

        public bool InsertarVenta(VentaRequestDto req)
        {
            var insert = new Domain.Entities.Venta //TODO normalizar import de obj
            {
                CentroDistribucionId = req.CentroDistribucionId,
                Detalles = new List<VentaDetalle>{
                    new VentaDetalle 
                    {
                        Cantidad = 2,
                        ImpuestoExtraUnitario = 0,
                        ModeloId = new Guid(),
                        PrecioUnitarioLista = 100
                    }
                },
                Fecha = req.Fecha,
            };
            
            //TODO utilizar automapper
            return _ventaRepository.insert(insert);
        }


        public IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, decimal> ObtenerVolumenPorCentro(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

        public decimal ObtenerVolumenTotal(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

        public string test()
        {
            return "service ok";
        }
    }
}
