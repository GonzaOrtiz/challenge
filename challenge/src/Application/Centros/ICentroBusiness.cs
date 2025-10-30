using challenge.src.Domain.Entities;

namespace challenge.src.Application.Centros
{
    public interface ICentroBusiness
    {
        IEnumerable<CentroDistribucion> GetAll();
        CentroDistribucion? GetById(Guid id);
    }
}
