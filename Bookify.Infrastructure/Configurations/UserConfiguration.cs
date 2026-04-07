using Bookify.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookify.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(user => user.FirstName).HasMaxLength(200).HasConversion(firstName => firstName.value, value => new FirstName(value));
        
        builder.Property(user => user.LastName).HasMaxLength(200).HasConversion(lastName => lastName.value, value => new LastName(value));
        
        builder.Property(user => user.Email).HasMaxLength(400).HasConversion(email => email.value, value => new Domain.User.Email(value));
        
        builder.HasIndex(user => user.Email).IsUnique();

    }
}