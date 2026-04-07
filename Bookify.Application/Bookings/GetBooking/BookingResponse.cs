namespace Bookify.Application.Bookings.GetBooking;

public class BookingResponse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ApartmentId { get; set; }

    public int Status { get; set; }

    public decimal PriceAmount { get; set; }

    public string PriceCurrency { get; set; }

    public decimal CleaningFeeAmount { get; set; }

    public string CleaningFeeCurrency { get; set; }

    public decimal AmenitiesUpChargeAmount { get; set; }

    public string AmenitiesUpChargeCurrency { get; set; }

    public decimal TotalPriceAmount { get; set; }

    public string TotalPriceCurrency { get; set; }
}