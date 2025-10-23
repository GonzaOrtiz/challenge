using challenge.src.Api.Dtos;

namespace challenge.src.Application.Venta
{
    public interface IVentaServices
    {
        string test();

        bool InsertarVenta(VentaRequestDto req);

        decimal ObtenerVolumenTotal(Guid? centroId = null);

        IDictionary<string, decimal> ObtenerVolumenPorCentro(Guid? centroId = null);

        IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
