using SkrinShop.Console.Core;
using SkrinShop.Persistence.Products;

namespace SkrinShop.Console.Products;
internal class ProductMapper : IMapper<ProductDto, Product>
{
    public Product Map(ProductDto input) => new()
    {
        Name = input.Name,
        Price = Convert.ToInt32(input.Price * 100)
    };
}
