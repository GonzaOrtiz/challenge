namespace challenge.src.Infrastructure.Venta
{
    public interface IVentaRepository
    {
        bool insert(Domain.Entities.Venta venta);
    }
}
