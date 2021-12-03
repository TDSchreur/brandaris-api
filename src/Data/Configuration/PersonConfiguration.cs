using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        if (builder is null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasKey(x => x.Id);

        builder.ToTable(nameof(Person));

        builder.Property(x => x.FirstName)
               .HasMaxLength(20);

        builder.Property(x => x.LastName)
               .HasMaxLength(20);
    }
}
