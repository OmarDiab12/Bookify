using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking;

public record BookingRejectedDomainEvent(Guid Id) : IDomainEvents;