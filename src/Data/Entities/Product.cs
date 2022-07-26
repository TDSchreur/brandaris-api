using System.Collections.Generic;

namespace Brandaris.Data.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }

    public ICollection<OrderLine> Orderlines { get; set; } = new HashSet<OrderLine>();
}