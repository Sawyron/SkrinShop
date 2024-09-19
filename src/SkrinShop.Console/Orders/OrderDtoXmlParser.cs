using SkrinShop.Console.Core;
using SkrinShop.Console.Products;
using SkrinShop.Console.Users;
using System.Collections.Immutable;
using System.Globalization;
using System.Xml.Linq;

namespace SkrinShop.Console.Orders;
public class OrderDtoXmlParser : IXmlParser<OrderDto>
{
    private readonly IXmlParser<ProductDto> _productParser;
    private readonly IXmlParser<UserDto> _userParser;

    public OrderDtoXmlParser(IXmlParser<ProductDto> productParser, IXmlParser<UserDto> userParser) =>
        (_productParser, _userParser) = (productParser, userParser);

    public OrderDto? Parse(XElement element)
    {
        if (!int.TryParse(element.Element("no")?.Value, out int id))
        {
            return null;
        }
        if (!DateTime.TryParse(element.Element("reg_date")?.Value, out DateTime date))
        {
            return null;
        }
        if (!double.TryParse(element.Element("sum")?.Value, CultureInfo.InvariantCulture, out double sum))
        {
            return null;
        }
        XElement? userNode = element.Element("user");
        if (userNode is null)
        {
            return null;
        }
        UserDto? user = _userParser.Parse(userNode);
        if (user is null)
        {
            return null;
        }
        List<ProductDto> products = [];
        foreach (XElement productNode in element.Elements("product"))
        {
            ProductDto? product = _productParser.Parse(productNode);
            if (product is null)
            {
                return null;
            }
            products.Add(product);
        }
        return new OrderDto(
            id,
            date,
            sum,
            user,
            products.ToImmutableList());
    }
}
