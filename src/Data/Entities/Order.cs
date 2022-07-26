using System.Collections.Generic;

namespace Brandaris.Data.Entities;

public class Order : EntityBase
{
    public ICollection<OrderLine> Orderlines { get; set; } = new HashSet<OrderLine>();

    public Person Person { get; set; }

    public int PersonId { get; set; }
}