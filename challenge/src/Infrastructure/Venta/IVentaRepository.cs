using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure.Ventas
{
    public interface IVentaRepository
    {
        bool insert(Venta venta);
        IEnumerable<Venta> GetAll(Guid? centroId = null);
        decimal GetVolumenTotal();
        decimal GetVolumenPorCentro(Guid centroId);
        IDictionary<string, decimal> GetPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
