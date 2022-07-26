using System;
using Brandaris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brandaris.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable(nameof(Order), x => x.IsTemporal());

        builder.HasOne(x => x.Person)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.PersonId);
    }
}