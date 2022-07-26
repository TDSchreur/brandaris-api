using System;

namespace Brandaris.Data;

public interface IAuditable
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
}