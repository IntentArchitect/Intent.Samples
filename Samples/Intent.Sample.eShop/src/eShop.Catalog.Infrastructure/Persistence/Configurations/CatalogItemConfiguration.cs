using eShop.Catalog.Domain.Entities;
using Intent.RoslynWeaver.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.EntityFrameworkCore.EntityTypeConfiguration", Version = "1.0")]

namespace eShop.Catalog.Infrastructure.Persistence.Configurations
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CatalogBrandId)
                .IsRequired();

            builder.Property(x => x.CatalogTypeId)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.PictureFileName)
                .IsRequired();

            builder.Property(x => x.PictureUri)
                .IsRequired();

            builder.Property(x => x.AvailableStock)
                .IsRequired();

            builder.Property(x => x.RestockThreshold)
                .IsRequired();

            builder.Property(x => x.MaxStockThreshold)
                .IsRequired();

            builder.Property(x => x.OnReorder)
                .IsRequired();

            builder.HasOne(x => x.CatalogBrand)
                .WithMany()
                .HasForeignKey(x => x.CatalogBrandId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CatalogType)
                .WithMany()
                .HasForeignKey(x => x.CatalogTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}