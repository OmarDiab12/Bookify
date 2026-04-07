using Bookify.Domain.Apartments;

namespace Bookify.Domain.Booking;

public class PricingService
{
    public static PricingDetails CalculatePrice(Apartment apartment, DateRange period)
    {
        var currency = apartment.Price.Currency;

        var priceForPeriod = new Money(apartment.Price.Amount * period.LengthInDays, currency);

        decimal percentageUpCharge = 0;
        foreach (var amentity in apartment.Amenities)
        {
            percentageUpCharge += amentity switch
            {
                Amenity.GardenView or Amenity.MountainView => .05m,
                Amenity.AirConditioning => .01m,
                Amenity.Parking => .01m,
                _ => 0
            };
        }
        var amentitiesUpCharge = Money.Zero(currency);

        if (percentageUpCharge > 0)
        {
            amentitiesUpCharge = new Money(percentageUpCharge * priceForPeriod.Amount, currency);
        }
        var totalPrice = Money.Zero();
        totalPrice += priceForPeriod;

        if (!apartment.CleaningFee.IsZero())
        {
            totalPrice += apartment.CleaningFee;
        }
        
        totalPrice += amentitiesUpCharge;
        return new PricingDetails(priceForPeriod, apartment.CleaningFee, amentitiesUpCharge, totalPrice);
    }
}