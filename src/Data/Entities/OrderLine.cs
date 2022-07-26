namespace Brandaris.Data.Entities;

public class OrderLine : EntityBase
{
    public Order Order { get; set; }

    public int OrderId { get; set; }

    public Product Product { get; set; }

    public int ProductId { get; set; }
}