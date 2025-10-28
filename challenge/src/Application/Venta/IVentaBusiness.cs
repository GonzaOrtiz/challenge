using challenge.src.Api.Dtos;

namespace challenge.src.Application.Ventas
{
    public interface IVentaBusiness
    {
        bool InsertarVenta(VentaRequestDto req);
        decimal ObtenerVolumenTotal();
        decimal ObtenerVolumenPorCentro(Guid centroId);
        IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
