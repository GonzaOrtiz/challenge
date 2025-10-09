namespace challenge.Domain.Entities
{
    public abstract class BaseEntidad
    {
        public Guid Id { get; set; }
        public DateTime FechaCreacionUtc { get; protected set; }

        protected BaseEntidad() { }

        protected BaseEntidad(Guid? id, DateTime fechaCreacionUtc)
        {
            Id = id ?? Guid.NewGuid();
            FechaCreacionUtc = fechaCreacionUtc;
        }
    }
}
