using Intent.RoslynWeaver.Attributes;
using Intent.Sample.Angular.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace Intent.Sample.Angular.Backend.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TitleId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Surname)
                .IsRequired();

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.OwnsMany(x => x.Addresses, ConfigureAddresses);

            builder.OwnsOne(x => x.Loyalty, ConfigureLoyalty);

            builder.HasOne(x => x.Title)
                .WithMany()
                .HasForeignKey(x => x.TitleId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public static void ConfigureAddresses(OwnedNavigationBuilder<Customer, Address> builder)
        {
            builder.WithOwner()
                .HasForeignKey(x => x.CustomerId);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.Line1)
                .IsRequired();

            builder.Property(x => x.Line2)
                .IsRequired();

            builder.Property(x => x.City)
                .IsRequired();

            builder.Property(x => x.Postal)
                .IsRequired();

            builder.Property(x => x.AddressType)
                .IsRequired();
        }

        public static void ConfigureLoyalty(OwnedNavigationBuilder<Customer, Loyalty> builder)
        {
            builder.WithOwner()
                .HasForeignKey(x => x.Id);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.LoyaltyNo)
                .IsRequired();

            builder.Property(x => x.Points)
                .IsRequired();
        }
    }
}