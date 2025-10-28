namespace challenge.src.Infrastructure.Venta
{
    public interface IVentaRepository
    {
        bool insert(Domain.Entities.Venta venta);
        IEnumerable<Domain.Entities.Venta> GetAll(Guid? centroId = null);
    IDictionary<string, decimal> GetPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
