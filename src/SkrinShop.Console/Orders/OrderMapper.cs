using SkrinShop.Console.Core;
using SkrinShop.Console.Users;
using SkrinShop.Persistence.Orders;
using SkrinShop.Persistence.Users;

namespace SkrinShop.Console.Orders;
internal class OrderMapper : IMapper<OrderDto, Order>
{
    private readonly IMapper<UserDto, User> _userMapper;

    public OrderMapper(IMapper<UserDto, User> userMapper)
    {
        _userMapper = userMapper;
    }

    public Order Map(OrderDto input) => new()
    {
        Id = input.Id,
        Date = input.Date,
        User = _userMapper.Map(input.User),
    };
}
