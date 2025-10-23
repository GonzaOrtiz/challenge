namespace challenge.src.Domain.Entities
{
    public class Venta : BaseEntidad
    {
        public Guid CentroDistribucionId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public List<VentaDetalle> Detalles { get; set; } = new();
        public decimal Total => Detalles.Sum(d => d.Subtotal);
    }
}
