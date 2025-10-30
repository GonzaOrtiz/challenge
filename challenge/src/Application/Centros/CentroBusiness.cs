using challenge.src.Domain.Entities;
using challenge.src.Infrastructure.Centros;

namespace challenge.src.Application.Centros
{
    public class CentroBusiness : ICentroBusiness
    {
        private readonly ICentroRepository _centroRepository;


        public CentroBusiness(ICentroRepository centroRepository)
        {
            _centroRepository = centroRepository;
        }

        public IEnumerable<CentroDistribucion> GetAll()
        {
            return _centroRepository.GetAll();
        }

        public CentroDistribucion? GetById(Guid id)
        {
            return _centroRepository.GetById(id);
        }
    }
}
