using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure.Modelos
{
    public class ModeloRepository : IModeloRepository
    {
        private static readonly object _lock = new();

        public Modelo? GetById(Guid id)
        {
            lock (_lock)
            {
                return InMemoryData.Modelos.FirstOrDefault(m => m.Id == id);
            }
        }

        public IEnumerable<Modelo> GetAll()
        {
            lock (_lock)
            {
                return InMemoryData.Modelos.ToList();
            }
        }
    }
}