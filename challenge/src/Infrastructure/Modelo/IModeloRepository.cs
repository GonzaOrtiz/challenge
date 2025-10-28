using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure.Modelo
{
    public interface IModeloRepository
    {

        Domain.Entities.Modelo? GetById(Guid id);
        IEnumerable<Domain.Entities.Modelo> GetAll();
    }
}