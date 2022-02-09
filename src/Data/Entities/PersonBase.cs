using System.Collections.Generic;

namespace Brandaris.Data.Entities;

public abstract class PersonBase : EntityBase
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; }
}
