using SkrinShop.Console.Orders;
using SkrinShop.Console.Products;
using SkrinShop.Console.Users;
using System.Xml.Linq;

namespace SkrinShop.Console.Test.Orders;

public class OrderXmlParserTest
{

    [Fact]
    public void WhenXmlNodeDataIsCorrent_ThenShouldReturnCorrectResult()
    {
        var doc = XDocument.Parse("""
            <order>
                    <no>1</no>
                    <reg_date>2012.12.19</reg_date>
                    <sum>234022.25</sum>
                    <product>
                        <quantity>2</quantity>
                        <name>LG 1755</name>
                        <price>12000.75</price>
                    </product>
                    <product>
                        <quantity>5</quantity>
                        <name>Xiomi 12X</name>
                        <price>42000.75</price>
                    </product>
                    <product>
                        <quantity>10</quantity>
                        <name>Noname 14232</name>
                        <price>1.7</price>
                    </product>
                    <user>
                        <fio>Иванов Иван Иванович</fio>
                        <email>abc@email.com</email>
                    </user>
                </order>
            """);
        XElement orderNode = doc.Element("order")!;
        Assert.NotNull(orderNode);
        var parser = new OrderDtoXmlParser(new ProductDtoXmlParser(), new UserDtoXmlParser());
        OrderDto order = parser.Parse(orderNode)!;
        Assert.NotNull(order);
        Assert.Equal(1, order.Id);
        Assert.Equal(new DateTime(2012, 12, 19).Date, order.Date);
        Assert.Equal(234022.25, order.Sum);
        Assert.Equal(new UserDto("Иванов Иван Иванович", "abc@email.com"), order.User);
        List<ProductDto> expectedProducts =
            [
                new ProductDto("LG 1755", 2, 12000.75),
                new ProductDto("Xiomi 12X", 5, 42000.75),
                new ProductDto("Noname 14232", 10, 1.7)
            ];
        Assert.Equal(expectedProducts.Count, order.Products.Count);
        Assert.Equal(expectedProducts, order.Products);
    }
}