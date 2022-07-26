using System;
using Brandaris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brandaris.Data.Configuration;

public class PersonPreCheckConfiguration : IEntityTypeConfiguration<PersonPreCheck>
{
    public void Configure(EntityTypeBuilder<PersonPreCheck> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable(nameof(PersonPreCheck), x => x.IsTemporal());

        builder.Property(x => x.FirstName)
               .HasMaxLength(20);

        builder.Property(x => x.LastName)
               .HasMaxLength(20);
    }
}