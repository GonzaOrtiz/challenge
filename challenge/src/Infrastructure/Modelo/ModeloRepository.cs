namespace challenge.src.Infrastructure.Modelo
{
    public class ModeloRepository : IModeloRepository
    {
        private static readonly object _lock = new();

        public Domain.Entities.Modelo? GetById(Guid id)
        {
            lock (_lock)
            {
                return InMemoryData.Modelos.FirstOrDefault(m => m.Id == id);
            }
        }

        public IEnumerable<Domain.Entities.Modelo> GetAll()
        {
            lock (_lock)
            {
                return InMemoryData.Modelos.ToList();
            }
        }
    }
}