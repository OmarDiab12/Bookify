using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Booking;
using Bookify.Domain.Booking.Events;
using Bookify.Domain.User;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler(
    IBookingRepository bookingRepository,
    IUserRepository userRepository,
    IEmailService emailService)
    : INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;


    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        var booking = await _bookingRepository.GetByIdAsync(notification.Id, cancellationToken);
        if (booking == null) return;
        
        var user = await _userRepository.GetByIdAsync(booking.UserId, cancellationToken);
        if (user == null) return;

        await _emailService.SendAsync(user.Email, "Booking reserved!", "You have 10 minutes to confirm this booking.");
    }
}