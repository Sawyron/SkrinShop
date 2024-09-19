using SkrinShop.Console.Products;
using System.Xml.Linq;

namespace SkrinShop.Console.Test.Produtcts;

public class ProductDtoXmlParserTest
{
    [Fact]
    public void WhenNodeDataIsCorrect_ThenParseShouldReturnCorrectResult()
    {
        var document = XDocument.Parse("""
            <product>
                <quantity>2</quantity>
                <name>LG 1755</name>
                <price>12000.75</price>
            </product>
            """);
        var parser = new ProductDtoXmlParser();
        XElement productNode = document.Element("product")!;
        Assert.NotNull(productNode);
        ProductDto? product = parser.Parse(productNode!);
        Assert.NotNull(product);
        Assert.Equal(2, product.Quantity);
        Assert.Equal("LG 1755", product.Name);
        Assert.Equal(12000.75, product.Price, 0.00001);
    }
}
