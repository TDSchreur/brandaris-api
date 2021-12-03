using DataAccess;

namespace Data.Entities;

public class OrderLine : IEntity
{
    public Order Order { get; set; }

    public int OrderId { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }

    public int Id { get; set; }
}
