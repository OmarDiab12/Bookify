namespace Bookify.Api.Controllers.BookingsController;

public sealed record ReserveBookingRequest(Guid ApartmentId, Guid UserId, DateOnly StartDate, DateOnly EndDate);