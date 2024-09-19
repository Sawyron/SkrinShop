using SkrinShop.Console.Products;
using SkrinShop.Console.Users;
using System.Collections.Immutable;

namespace SkrinShop.Console.Orders;

public sealed record OrderDto(
    int Id,
    DateTime Date,
    double Sum,
    UserDto User,
    IImmutableList<ProductDto> Products);
