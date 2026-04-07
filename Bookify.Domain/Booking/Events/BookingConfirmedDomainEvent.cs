using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Booking;

public record BookingConfirmedDomainEvent(Guid Id) : IDomainEvents;