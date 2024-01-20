namespace MusicPracticePlanner.Base.DomainPrimitives;


public abstract class Entity : Entity<Guid>
{
    protected Entity(Guid id) : base(id)
    {}
}

public abstract class Entity<Type>
{
    private List<IDomainEvent> _domainEvents = new();
    public Type Id{get; private set;}

    protected Entity(Type id)
    {
        Id = id;
    }

    public IList<IDomainEvent> DomainEvents => _domainEvents;
    public void Raise(IDomainEvent domainEvent){
        this._domainEvents.Add(domainEvent);
    }
    public void ClearEvents(){
        this._domainEvents.Clear();
    }

}
