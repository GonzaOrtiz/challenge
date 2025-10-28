using System;
using challenge.src.Api.Dtos;

namespace challenge.src.Application.Venta
{
    public interface IVentaBusiness
    {
        bool InsertarVenta(VentaRequestDto req);

        /// <summary>
        /// Obtiene el volumen total de ventas (todas las ventas)
        /// </summary>
        decimal ObtenerVolumenTotal();

        /// <summary>
        /// Obtiene el volumen de ventas filtrado por centro (ID obligatorio)
        /// </summary>
        decimal ObtenerVolumenPorCentro(Guid centroId);

        IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
