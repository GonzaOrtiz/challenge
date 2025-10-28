

using AutoMapper;
using challenge.src.Api.Dtos;
using challenge.src.Infrastructure.Venta;

namespace challenge.src.Application.Venta
{
    public class VentaBusiness : IVentaBusiness
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;
        public VentaBusiness(IVentaRepository ventaRepository, IMapper mapper)
        {
            _ventaRepository = ventaRepository;
            _mapper = mapper;
        }

        public bool InsertarVenta(VentaRequestDto req)
        {
            var venta = _mapper.Map<Domain.Entities.Venta>(req);
            return _ventaRepository.insert(venta);
        }

        public decimal ObtenerVolumenTotal(Guid? centroId = null)
        {
            var ventas = _ventaRepository.GetAll(centroId);
            decimal volumenTotal = 0;
            foreach (var venta in ventas)
            {
                volumenTotal += venta.Total;
            }
            return volumenTotal;
        }

        public IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null)
        {
            return _ventaRepository.GetPorcentajeModelosPorCentro(centroId);
        }

        public IDictionary<string, decimal> ObtenerVolumenPorCentro(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

    }
}
