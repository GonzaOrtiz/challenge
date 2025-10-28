namespace challenge.src.Infrastructure.Venta
{
    public class VentaRepository : IVentaRepository
    {
        private static readonly object _lock = new();
        public bool insert(Domain.Entities.Venta venta)
        {
            try
            {
                lock (_lock)
                {
                    InMemoryData.Ventas.Add(venta);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Domain.Entities.Venta> GetAll(Guid? centroId = null)
        {
            lock (_lock)
            {
                if (centroId.HasValue)
                {
                    return InMemoryData.Ventas.Where(v => v.CentroDistribucionId == centroId).ToList();
                }
                return InMemoryData.Ventas.ToList();
            }
        }
    }
}
