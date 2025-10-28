using AutoMapper;
using challenge.src.Api.Dtos;
using challenge.src.Domain.Entities;

namespace challenge.src.Mapping
{
    public class VentaProfile : Profile
    {
        public VentaProfile()
        {
            CreateMap<VentaRequestDto, Venta>();
            CreateMap<VentaDetalleDto, VentaDetalle>();
        }
    }
}
