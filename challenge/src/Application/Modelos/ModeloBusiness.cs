using challenge.src.Domain.Entities;
using challenge.src.Domain.Constants;
using challenge.src.Infrastructure.Modelos;

namespace challenge.src.Application.Modelos
{
    public class ModeloBusiness : IModeloBusiness
    {
        private readonly IModeloRepository _modeloRepository;
        private static readonly object _lock = new();

        public ModeloBusiness(IModeloRepository modeloRepository)
        {
            _modeloRepository = modeloRepository;
        }

        public IEnumerable<Modelo> GetAll()
        {
            lock (_lock)
            {
                return _modeloRepository.GetAll();
            }
        }

        public Modelo? GetById(Guid id)
        {
            lock (_lock)
            {
                return _modeloRepository.GetById(id);
            }
        }

        public decimal CalcularImpuestoExtraUnitario(Modelo modelo)
        {
            if (modelo == null) return 0;
            if (modelo.Tipo == Domain.Enums.TipoModelo.Sport)
            {
                return modelo.PrecioBase * ImpuestosConstantes.IMPUESTO_EXTRA_SPORT;
            }

            return 0;
        }
    }
}
