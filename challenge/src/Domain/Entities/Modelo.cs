using challenge.src.Domain.Enums;

namespace challenge.src.Domain.Entities
{
    public class Modelo : BaseEntidad
    {
        public string Nombre { get; set; }
        public TipoModelo Tipo { get; set; }
        public decimal PrecioBase { get; set; }
        public TipoMoneda Moneda { get; set; } = TipoMoneda.USD;
    }
}
