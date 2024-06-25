namespace Store.Domain;

public class EntityId : TinyType<Guid>
{
    public EntityId() : this(NewGuid())
    {
    }

    public EntityId(Guid value) : base(value)
    {
    }
}
