using Bookify.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

internal sealed class AprtmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.ToTable("apartments");
        
        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.Address);
        
        builder.Property(x => x.Name).HasMaxLength(200).HasConversion(name => name.Value, value => new Name(value));
        
        builder.Property(x => x.Description).HasMaxLength(200).HasConversion(description => description.Value, value => new Description(value));
        
        builder.OwnsOne(apartment => apartment.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.code, code => Currency.FromCode(code));
            }
        );

        builder.Property<uint>("Version").IsRowVersion();
    }
}