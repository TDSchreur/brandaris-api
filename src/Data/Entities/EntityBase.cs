using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Brandaris.Data.Entities;

public abstract class EntityBase : IEntity, IAuditable
{
    public string ApprovedBy { get; set; }

    public Guid? ApprovedById { get; set; }

    public DateTimeOffset? ApprovedDate { get; set; }

    public string CreatedBy { get; set; }

    public Guid CreatedById { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public Guid? UpdatedById { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
}
