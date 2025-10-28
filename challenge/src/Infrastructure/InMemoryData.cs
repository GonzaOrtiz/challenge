using challenge.src.Domain.Entities;

namespace challenge.src.Infrastructure
{
    public static class InMemoryData
    {
        public static readonly System.Collections.Concurrent.ConcurrentBag<Venta> Ventas = new();
        public static readonly List<Modelo> Modelos = new()
        {
            new Modelo { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Nombre = "Sedan", Tipo = Domain.Enums.TipoModelo.Sedan, PrecioBase = 8000 },
            new Modelo { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Nombre = "Suv", Tipo = Domain.Enums.TipoModelo.Suv, PrecioBase = 9500 },
            new Modelo { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Nombre = "Offroad", Tipo = Domain.Enums.TipoModelo.Offroad, PrecioBase = 12500 },
            new Modelo { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Nombre = "Sport", Tipo = Domain.Enums.TipoModelo.Sport, PrecioBase = 18200 }
        };
    }
}
