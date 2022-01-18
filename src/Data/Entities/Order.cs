using System.Collections.Generic;
using DataAccess;

namespace Data.Entities;

public class Order : IEntity
{
    public ICollection<OrderLine> Orderlines { get; set; } = new HashSet<OrderLine>();

    public Person Person { get; set; }

    public int PersonId { get; set; }

    public int Id { get; set; }
}
