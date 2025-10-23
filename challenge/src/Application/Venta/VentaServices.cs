

namespace challenge.src.Application.Venta
{
    public class VentaServices : IVentaServices
    {
        public VentaServices() { }

        public void InsertarVenta(Guid centroId, IEnumerable<(Guid modeloId, int cantidad)> items)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, decimal> ObtenerPorcentajeModelosPorCentro(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, decimal> ObtenerVolumenPorCentro(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

        public decimal ObtenerVolumenTotal(Guid? centroId = null)
        {
            throw new NotImplementedException();
        }

        public string test()
        {
            return "service ok";
        }
    }
}
