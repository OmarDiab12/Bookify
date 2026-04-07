using System.Data;
using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Booking;
using Bookify.Domain.User;

namespace Bookify.Application.Bookings.ReserveBooking;

public sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IApartmentRepository _apartmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookingRepository _bookingRepository;
    private readonly PricingService _pricingService;
    private readonly IDateTimeProvider _dateTimeProvider;
    


    public ReserveBookingCommandHandler(IApartmentRepository apartmentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IBookingRepository bookingRepository, PricingService pricingService, IDateTimeProvider dateTimeProvider)
    {
        _apartmentRepository = apartmentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _bookingRepository = bookingRepository;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
        _bookingRepository = bookingRepository;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is  null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        Apartment? apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);
        if (apartment is null)
        {
            return Result.Failure<Guid>(ApartmentErrors.NotFound);
        }
        
        DateRange duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _bookingRepository.IsOverLappingAsync(apartment, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }

        try
        {
            Booking booking = Booking.Reserve(apartment, user.Id, duration, _dateTimeProvider.UtcNow, _pricingService);

            _bookingRepository.Add(booking);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        
            return booking.Id;
        }
        catch (ConcurrencyException)
        {
            return Result.Failure<Guid>(BookingErrors.Overlap);
        }
        
    }
}