using Bookify.Domain.Apartments;
using Bookify.Domain.Booking;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repository;

internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
{
    private static readonly BookingStatus[] ActiveBookingStatuses =
    [
        BookingStatus.Completed,
        BookingStatus.Confirmed,
        BookingStatus.Reserved
    ];
    
    public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<bool> IsOverLappingAsync(Apartment apartment, DateRange duration, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Booking>().AnyAsync(booking => 
                                                        booking.ApartmentId == apartment.Id &&
                                                        booking.Duration.Start == duration.Start &&
                                                        booking.Duration.End == duration.End &&
                                                        ActiveBookingStatuses.Contains(booking.Status), cancellationToken);
    }
}