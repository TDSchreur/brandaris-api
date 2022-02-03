using System;
using Brandaris.DataAccess;

namespace Brandaris.Data.Entities;

public abstract class EntityBase : IEntity, IAuditable
{
    public int Id { get; set; }

    public string CreatedBy { get; set; }

    public Guid CreatedById { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public Guid? UpdatedById { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
}
