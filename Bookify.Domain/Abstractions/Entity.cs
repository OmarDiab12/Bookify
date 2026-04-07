namespace Bookify.Domain.Abstractions;

public abstract class Entity(Guid id)
{
    private readonly List<IDomainEvents>  _domainEvents = new();
    public Guid Id { get; init; } = id;

    public IReadOnlyCollection<IDomainEvents> GetDomainEvents() => _domainEvents.ToList();
    
    public void ClearDomainEvents() => _domainEvents.Clear();

    public void RaiseDomainEvent(IDomainEvents domainEvent) => _domainEvents.Add(domainEvent);
}