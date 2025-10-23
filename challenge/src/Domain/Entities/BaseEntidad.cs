namespace challenge.src.Domain.Entities
{
    //Clase base para las entidades del dominio
    public abstract class BaseEntidad
    {
        public Guid Id { get; set; }

        protected BaseEntidad() { }

        protected BaseEntidad(Guid? id)
        {
            Id = id ?? Guid.NewGuid();
        }
    }
}
