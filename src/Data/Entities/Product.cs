using System.Collections.Generic;
using DataAccess;

namespace Data.Entities;

public class Product : IEntity
{
    public string Name { get; set; }

    public ICollection<OrderLine> Orderlines { get; set; } = new HashSet<OrderLine>();

    public int Id { get; set; }
}
