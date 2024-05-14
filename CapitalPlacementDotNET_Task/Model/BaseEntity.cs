namespace CapitalPlacementDotNET_Task.Model
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();

        }
        public Guid Id { get; set; }

    }
}
