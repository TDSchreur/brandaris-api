using System;

namespace Brandaris.DataAccess;

public interface IAuditable
{
    public string CreatedBy { get; set; }

    public Guid CreatedById { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public Guid? UpdatedById { get; set; }

    public DateTimeOffset? UpdatedDate { get; set; }
}
