using SkrinShop.Persistence.Orders;
using SkrinShop.Persistence.Products;

namespace SkrinShop.Persistence.OrderItems;

public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public Product Product { get; set; } = default!;
    public int ProductId { get; set; }
    public Order Order { get; set; } = default!;
    public int OrderId { get; set; }
}
