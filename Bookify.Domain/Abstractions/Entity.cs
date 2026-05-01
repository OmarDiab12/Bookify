namespace Bookify.Domain.Abstractions;

public abstract class Entity
{
    public Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
        
    }
    private readonly List<IDomainEvents>  _domainEvents = new();
    public Guid Id { get; init; }

    public IReadOnlyCollection<IDomainEvents> GetDomainEvents() => _domainEvents.ToList();
    
    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvents domainEvent) => _domainEvents.Add(domainEvent);
}