using System;

namespace challenge.src.Infrastructure.Venta
{
    public class VentaRepository : IVentaRepository
    {
        private static readonly object _lock = new();

        public bool insert(Domain.Entities.Venta venta)
        {
            try
            {
                lock (_lock)
                {
                    InMemoryData.Ventas.Add(venta);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Domain.Entities.Venta> GetAll(Guid? centroId = null)
        {
            lock (_lock)
            {
                if (centroId.HasValue)
                {
                    return InMemoryData.Ventas.Where(v => v.CentroDistribucionId == centroId).ToList();
                }
                return InMemoryData.Ventas.ToList();
            }
        }

        public decimal GetVolumenTotal()
        {
            lock (_lock)
            {
                return InMemoryData.Ventas.Sum(v => v.Total);
            }
        }

        public decimal GetVolumenPorCentro(Guid centroId)
        {
            lock (_lock)
            {
                return InMemoryData.Ventas
                    .Where(v => v.CentroDistribucionId == centroId)
                    .Sum(v => v.Total);
            }
        }

        public IDictionary<string, decimal> GetPorcentajeModelosPorCentro(Guid? centroId = null)
        {
            lock (_lock)
            {
                // Obtener todas las ventas para el centro especificado o todos los centros
                var ventas = GetAll(centroId);
                var totalUnidades = ventas.Sum(v => v.Detalles.Count);

                // Si no hay ventas, retornar diccionario vacío
                if (totalUnidades == 0)
                    return new Dictionary<string, decimal>();

                // Crear diccionario de búsqueda rápida para nombres de modelos
                var modelosMemoria = InMemoryData.Modelos.ToDictionary(m => m.Id, m => m.Nombre);

                // Calcular los porcentajes
                return ventas
                    .SelectMany(v => v.Detalles)
                    .GroupBy(d => d.ModeloId)
                    .ToDictionary(
                        g => modelosMemoria.TryGetValue(g.Key, out var nombre) ? nombre : g.Key.ToString(),
                        g => Math.Round((decimal)g.Count() / totalUnidades * 100, 2)
                    );
            }
        }
    }
}
