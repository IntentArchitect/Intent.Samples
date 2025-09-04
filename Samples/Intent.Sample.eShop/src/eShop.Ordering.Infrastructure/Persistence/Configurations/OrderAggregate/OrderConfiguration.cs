using eShop.Ordering.Domain;
using eShop.Ordering.Domain.OrderAggregate;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace eShop.Ordering.Infrastructure.Persistence.Configurations.OrderAggregate
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderDate)
                .IsRequired();

            builder.Property(x => x.OrderStatus)
                .IsRequired();

            builder.Property(x => x.Description);

            builder.Property(x => x.IsDraft)
                .IsRequired();

            builder.Property(x => x.BuyerId);

            builder.OwnsMany(x => x.OrderItems, ConfigureOrderItems);

            builder.OwnsOne(x => x.Address, ConfigureAddress)
                .Navigation(x => x.Address).IsRequired();

            builder.HasOne(x => x.Buyer)
                .WithMany()
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.DomainEvents);
        }

        public static void ConfigureOrderItems(OwnedNavigationBuilder<Order, OrderItem> builder)
        {
            builder.WithOwner(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.Property(x => x.ProductName)
                .IsRequired();

            builder.Property(x => x.PictureUrl)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .IsRequired();

            builder.Property(x => x.Discount)
                .IsRequired();

            builder.Property(x => x.Units)
                .IsRequired();

            builder.Property(x => x.ProductId)
                .IsRequired();
        }

        public static void ConfigureAddress(OwnedNavigationBuilder<Order, Address> builder)
        {
            builder.WithOwner();

            builder.Property(x => x.Street)
                .IsRequired();

            builder.Property(x => x.City)
                .IsRequired();

            builder.Property(x => x.State)
                .IsRequired();

            builder.Property(x => x.Country)
                .IsRequired();

            builder.Property(x => x.ZipCode)
                .IsRequired();
        }
    }
}