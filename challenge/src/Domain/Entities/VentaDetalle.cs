namespace challenge.src.Domain.Entities
{
    public class VentaDetalle : BaseEntidad
    {
        public Guid ModeloId { get; init; }
        public int Cantidad { get; init; } = 1;
        public decimal PrecioUnitarioLista { get; init; }
        public decimal ImpuestoExtraUnitario { get; init; }
        public decimal Subtotal => (PrecioUnitarioLista + ImpuestoExtraUnitario) * Cantidad;
    }
}
