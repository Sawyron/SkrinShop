using SkrinShop.Console.Core;
using SkrinShop.Console.Products;
using SkrinShop.Persistence.OrderItems;
using SkrinShop.Persistence.Orders;

namespace SkrinShop.Console.Orders;
internal class OrderAndOrderItemsMapper : IMapper<OrderDto, (Order, List<OrderItem>)>
{
    private readonly IMapper<OrderDto, Order> _orderMapper;
    private readonly IMapper<ProductDto, OrderItem> _itemMapper;

    public OrderAndOrderItemsMapper(IMapper<OrderDto, Order> orderMapper, IMapper<ProductDto, OrderItem> itemMapper)
    {
        _orderMapper = orderMapper;
        _itemMapper = itemMapper;
    }

    public (Order, List<OrderItem>) Map(OrderDto input) =>
        (_orderMapper.Map(input), input.Products.Select(_itemMapper.Map).ToList());
}
