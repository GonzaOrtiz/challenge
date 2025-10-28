using challenge.src.Api.Dtos;

namespace challenge.src.Application.Venta
{
    public interface IVentaBusiness
    {
        bool InsertarVenta(VentaRequestDto req);

        decimal ObtenerVolumenTotal(Guid? centroId = null);

        IDictionary<string, decimal> ObtenerVolumenPorCentro(Guid? centroId = null);

        IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
