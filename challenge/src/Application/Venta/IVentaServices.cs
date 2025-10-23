namespace challenge.src.Application.Venta
{
    public interface IVentaServices
    {
        string test();

        void InsertarVenta(Guid centroId, IEnumerable<(Guid modeloId, int cantidad)> items);

        decimal ObtenerVolumenTotal(Guid? centroId = null);

        IDictionary<string, decimal> ObtenerVolumenPorCentro(Guid? centroId = null);

        IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null);
    }
}
