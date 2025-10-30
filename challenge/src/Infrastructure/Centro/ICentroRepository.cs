using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure.Centros
{
    public interface ICentroRepository
    {
        CentroDistribucion? GetById(Guid id);
        IEnumerable<CentroDistribucion> GetAll();
    }
}
