using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure.Modelos
{
    public interface IModeloRepository
    {

        Modelo? GetById(Guid id);
        IEnumerable<Modelo> GetAll();
    }
}