namespace challenge.Domain.Entities
{
    public class VentaDetalle
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public int ModeloId { get; init; }
        public int Cantidad { get; init; } = 1;
        public decimal PrecioUnitarioLista { get; init; }
        public decimal ImpuestoExtraUnitario { get; init; }
        public decimal Subtotal => (PrecioUnitarioLista + ImpuestoExtraUnitario) * Cantidad;
    }
}
