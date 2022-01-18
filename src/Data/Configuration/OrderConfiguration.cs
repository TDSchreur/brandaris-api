using System;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(Order), x => x.IsTemporal());

        builder.HasOne(x => x.Person)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.PersonId);
    }
}
