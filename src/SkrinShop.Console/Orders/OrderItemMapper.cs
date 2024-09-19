using SkrinShop.Console.Core;
using SkrinShop.Console.Products;
using SkrinShop.Persistence.OrderItems;
using SkrinShop.Persistence.Products;

namespace SkrinShop.Console.Orders;

internal class OrderItemMapper : IMapper<ProductDto, OrderItem>
{
    private readonly IMapper<ProductDto, Product> _productMapper;

    public OrderItemMapper(IMapper<ProductDto, Product> productMapper)
    {
        _productMapper = productMapper;
    }

    public OrderItem Map(ProductDto input) => new()
    {
        Price = Convert.ToInt32(input.Price),
        Quantity = input.Quantity,
        Product = _productMapper.Map(input)
    };
}
