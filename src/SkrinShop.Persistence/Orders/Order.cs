using SkrinShop.Persistence.Users;

namespace SkrinShop.Persistence.Orders;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public User User { get; set; } = default!;
    public int UserId { get; set; }
}
