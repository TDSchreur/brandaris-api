using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Common;
using Brandaris.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Brandaris.Data;

public class DataContext : DbContext
{
    private readonly IIdentityHelper _identityHelper;

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DataContext(DbContextOptions<DataContext> options, IIdentityHelper identityHelper) : base(options)
    {
        _identityHelper = identityHelper;
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<PersonPreCheck> PersonPreChecks { get; set; }

    public DbSet<Person> Persons { get; set; }

    public DbSet<Product> Products { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        /*
         * Opties:
         * Nieuw persoon aangemaakt     => PersonPreCheck   (Added)
         * Nieuw persoon goedgekeurd    => Person           (Added)
         * Bestaand persoon geupdate    => PersonPreCheck   (Modified)
         * Bestaand persoon goedgekeurd => Person           (Modified)
         */

        string name = _identityHelper.GetName() ?? "SeedTool";
        Guid oid = _identityHelper.GetOid();

        IEnumerable<EntityEntry<IAuditable>> changes = ChangeTracker.Entries<IAuditable>()
                                                                    .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (EntityEntry<IAuditable> entry in changes)
        {
            bool isPreCheck = entry.Entity is IPreCheck;

            switch (entry.State)
            {
                case EntityState.Added:
                    if (isPreCheck)
                    {
                        entry.Entity.CreatedBy = name;
                        entry.Entity.CreatedById = oid;
                        entry.Entity.CreatedDate = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entry.Entity.ApprovedBy = name;
                        entry.Entity.ApprovedById = oid;
                        entry.Entity.ApprovedDate = DateTimeOffset.UtcNow;
                    }

                    break;
                case EntityState.Modified:
                    if (isPreCheck)
                    {
                        entry.Entity.UpdatedBy = name;
                        entry.Entity.UpdatedById = oid;
                        entry.Entity.UpdatedDate = DateTimeOffset.UtcNow;
                    }
                    else
                    {
                        entry.Entity.ApprovedBy = name;
                        entry.Entity.ApprovedById = oid;
                        entry.Entity.ApprovedDate = DateTimeOffset.UtcNow;
                    }

                    break;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}