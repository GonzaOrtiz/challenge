namespace challenge.src.Infrastructure
{
    public static class InMemoryData
    {
        public static readonly System.Collections.Concurrent.ConcurrentBag<Domain.Entities.Venta> Ventas = new();
    }
}
