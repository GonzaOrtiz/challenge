namespace challenge.Domain.Entities
{
    public class Venta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int CentroDistribucionId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public List<VentaDetalle> Detalles { get; set; } = new();
        public decimal Total => Detalles.Sum(d => d.Subtotal);
    }
}
