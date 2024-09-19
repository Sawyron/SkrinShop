using SkrinShop.Console.Core;
using System.Globalization;
using System.Xml.Linq;

namespace SkrinShop.Console.Products;

public class ProductDtoXmlParser : IXmlParser<ProductDto>
{
    private const string ErrorTag = "Xml user parse error";

    public ProductDto? Parse(XElement element)
    {
        if (!int.TryParse(element.Element("quantity")?.Value, out int quantity))
        {
            return null;
        }
        string? name = element.Element("name")?.Value;
        if (name is null)
        {
            return null;
        }
        if (!double.TryParse(element.Element("price")?.Value, CultureInfo.InvariantCulture, out double price))
        {
            return null;
        }
        return new ProductDto(
            name,
            quantity,
            price);
    }
}
