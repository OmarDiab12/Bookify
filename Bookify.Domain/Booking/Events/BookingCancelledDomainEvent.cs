using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking;

public record BookingCancelledDomainEvent(Guid Id) : IDomainEvents;