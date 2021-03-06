using System;
using Brandaris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brandaris.Data.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable(nameof(Person), x => x.IsTemporal());

        builder.Property(x => x.FirstName)
               .HasMaxLength(20);

        builder.Property(x => x.LastName)
               .HasMaxLength(20);
    }
}