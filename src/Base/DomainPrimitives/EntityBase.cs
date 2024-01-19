namespace MusicPracticePlanner.Base.DomainPrimitives;


public abstract class Entity : Entity<Guid>
{
    protected Entity(Guid id) : base(id)
    {
    }
}

public abstract class Entity<Type>
{
    public Type Id{get; private set;}

    protected Entity(Type id)
    {
        Id = id;
    }
}
