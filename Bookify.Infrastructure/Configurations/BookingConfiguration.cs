using Bookify.Domain.Apartments;
using Bookify.Domain.Booking;
using Bookify.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        
        builder.HasKey(x => x.Id);

        builder.OwnsOne(booking => booking.PriceForPeriod, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currnecy => currnecy.code, code => Currency.FromCode(code));
        });
        
        builder.OwnsOne(booking => booking.CleaningFee, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currnecy => currnecy.code, code => Currency.FromCode(code));
        });
        
        builder.OwnsOne(booking => booking.AmentitiesUpCharge, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currnecy => currnecy.code, code => Currency.FromCode(code));
        });
        
        builder.OwnsOne(booking => booking.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currnecy => currnecy.code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(b => b.Duration);

        builder.HasOne<Apartment>().WithMany().HasForeignKey(b => b.ApartmentId);
        
        builder.HasOne<User>().WithMany().HasForeignKey(b => b.UserId);
    }
}