using System.ComponentModel.DataAnnotations;

namespace challenge.src.Api.Dtos
{
    public class VentaRequestDto
    {
        [Required]
        public Guid CentroDistribucionId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        [MinLength(1, ErrorMessage = "Se debe incluir al menos un detalle de venta.")]
        public List<VentaDetalleDto> Detalles { get; set; } = new();
    }

    public class VentaDetalleDto
    {
        [Required]
        public Guid ModeloId { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; init; } = 1;
    }
}
