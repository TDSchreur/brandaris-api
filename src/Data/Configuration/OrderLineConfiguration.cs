using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(OrderLine));

        builder.HasOne(x => x.Product)
               .WithMany(x => x.Orderlines)
               .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Order)
               .WithMany(x => x.Orderlines)
               .HasForeignKey(x => x.OrderId);
    }
}
