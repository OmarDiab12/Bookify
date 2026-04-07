using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking;

public record BookingCompletedDomainEvent(Guid Id) : IDomainEvents;