using System;

namespace challenge.src.Infrastructure.Venta
{
    public interface IVentaRepository
    {
        bool insert(Domain.Entities.Venta venta);
        IEnumerable<Domain.Entities.Venta> GetAll(Guid? centroId = null);
        decimal GetVolumenTotal();
        decimal GetVolumenPorCentro(Guid centroId);
        IDictionary<string, decimal> GetPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
