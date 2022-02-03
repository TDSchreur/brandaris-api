using System.Collections.Generic;

namespace Brandaris.Data.Entities;

public class Person : EntityBase
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; }
}
