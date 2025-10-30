using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure.Centros
{
    public class CentroRepository : ICentroRepository
    {
        private static readonly object _lock = new();

        public CentroDistribucion? GetById(Guid id)
        {
            lock (_lock)
            {
                return InMemoryData.Centros.FirstOrDefault(c => c.Id == id);
            }
        }

        public IEnumerable<CentroDistribucion> GetAll()
        {
            lock (_lock)
            {
                return InMemoryData.Centros.ToList();
            }
        }
    }
}
