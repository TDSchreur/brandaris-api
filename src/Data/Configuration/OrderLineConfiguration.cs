using System;
using Brandaris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brandaris.Data.Configuration;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(OrderLine), x => x.IsTemporal());

        builder.HasOne(x => x.Product)
               .WithMany(x => x.Orderlines)
               .HasForeignKey(x => x.ProductId);

        builder.HasOne(x => x.Order)
               .WithMany(x => x.Orderlines)
               .HasForeignKey(x => x.OrderId);
    }
}
