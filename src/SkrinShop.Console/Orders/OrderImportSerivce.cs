using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkrinShop.Console.Core;
using SkrinShop.Persistence;
using SkrinShop.Persistence.OrderItems;
using SkrinShop.Persistence.Orders;
using SkrinShop.Persistence.Products;
using SkrinShop.Persistence.Users;

namespace SkrinShop.Console.Orders;
public sealed class OrderImportSerivce
{
    private readonly ILogger<OrderImportSerivce> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper<OrderDto, (Order Order, List<OrderItem> Items)> _orderMapper;

    public OrderImportSerivce(
        ILogger<OrderImportSerivce> logger,
        ApplicationDbContext context,
        IMapper<OrderDto, (Order Order, List<OrderItem> Items)> orderMapper)
    {
        _logger = logger;
        _context = context;
        _orderMapper = orderMapper;
    }

    public async Task ImportOrderAsync(IEnumerable<OrderDto> orderDtos, CancellationToken token = default)
    {
        foreach (OrderDto order in orderDtos)
        {
            try
            {
                await PerformImport(order, token);
                await _context.SaveChangesAsync(token);
                _logger.LogInformation("Successfully imported order with {} positions", order.Products.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError("Order import error. Order id: {}. {}", order.Id, ex);
            }
        }
    }

    private async Task PerformImport(OrderDto orderDto, CancellationToken token)
    {
        Order? existingOrder = await _context.Set<Order>().FirstOrDefaultAsync(o => o.Id == orderDto.Id, token);
        if (existingOrder is not null)
        {
            throw new InvalidOperationException("Order already exists");
        }
        (Order order, List<OrderItem> items) = _orderMapper.Map(orderDto);
        User? user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == orderDto.User.Email, token);
        if (user is not null)
        {
            order.User = user;
            order.UserId = user.Id;
        }
        HashSet<string> productNames = items.Select(i => i.Product.Name).ToHashSet();
        Dictionary<string, Product> existingProducts = await _context.Set<Product>()
            .Where(p => productNames.Contains(p.Name))
            .ToDictionaryAsync(p => p.Name, p => p, token);
        foreach (OrderItem item in items)
        {
            if (existingProducts.TryGetValue(item.Product.Name, out Product? product))
            {
                item.Product = product;
            }
            item.Order = order;
        }
        await _context.AddAsync(order, token);
        await _context.AddRangeAsync(items, token);
    }
}
