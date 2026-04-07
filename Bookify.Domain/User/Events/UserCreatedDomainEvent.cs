using Bookify.Domain.Abstractions;

namespace Bookify.Domain.User.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvents;