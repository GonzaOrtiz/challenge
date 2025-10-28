namespace challenge.src.Api.Dtos
{
    public class VentaRequestDto
    {
        public Guid CentroDistribucionId { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public List<VentaDetalleDto> Detalles { get; set; } = new();
    }

    public class VentaDetalleDto
    {
        public Guid ModeloId { get; init; }
        public int Cantidad { get; init; } = 1;
    }
}
