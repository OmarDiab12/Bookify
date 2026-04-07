using Bookify.Domain.Apartments;

namespace Bookify.Domain.Booking;

public record PricingDetails(Money PriceForPeriod, Money CleaningFee, Money AmentitiesUpCharge, Money TotalPrice);