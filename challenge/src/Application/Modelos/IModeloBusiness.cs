using challenge.src.Domain.Entities;

namespace challenge.src.Application.Modelos
{
    public interface IModeloBusiness
    {
        IEnumerable<Modelo> GetAll();
        Modelo? GetById(Guid id);
        /// <summary>
        /// Calcula el impuesto extra por unidad para un modelo dado (por ejemplo, +7% para Sport).
        /// Devuelve el impuesto unitario (no multiplicado por cantidad).
        /// </summary>
        decimal CalcularImpuestoExtraUnitario(Modelo modelo);
    }
}
