using Microsoft.Extensions.DependencyInjection;
using SkrinShop.Console.Core;
using SkrinShop.Console.Orders;
using SkrinShop.Console.Products;
using SkrinShop.Console.Users;
using SkrinShop.Persistence.OrderItems;
using SkrinShop.Persistence.Orders;
using SkrinShop.Persistence.Products;
using SkrinShop.Persistence.Users;

namespace SkrinShop.Console;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IXmlParser<UserDto>, UserDtoXmlParser>();
        services.AddSingleton<IXmlParser<ProductDto>, ProductDtoXmlParser>();
        services.AddSingleton<IXmlParser<OrderDto>, OrderDtoXmlParser>();
        services.AddScoped<OrderImportSerivce>();
        services.AddSingleton<OrderReader>();
        services.AddSingleton<IMapper<OrderDto, (Order, List<OrderItem>)>, OrderAndOrderItemsMapper>();
        services.AddSingleton<IMapper<OrderDto, Order>, OrderMapper>();
        services.AddSingleton<IMapper<ProductDto, Product>, ProductMapper>();
        services.AddSingleton<IMapper<ProductDto, OrderItem>, OrderItemMapper>();
        services.AddSingleton<IMapper<UserDto, User>, UserMapper>();
        return services;
    }
}
