using Bookify.Domain.Apartments;

namespace Bookify.Domain.Booking;

public interface IBookingRepository
{
    Task<Booking?> GetByIdAsync(Guid id,  CancellationToken cancellationToken = default);
    Task<bool> IsOverLappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken);
    void Add(Booking booking);
}