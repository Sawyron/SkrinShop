using SkrinShop.Console.Users;
using System.Xml.Linq;

namespace SkrinShop.Console.Test.Users;

public class UserDtoXmlParserTest
{
    [Fact]
    public void WhenNodeDataIsCorrent_ThenSholdReturnCorrectResult()
    {
        var document = XDocument.Parse("""
            <user>
                <fio>Иванов Иван Иванович</fio>
                <email>abc@email.com</email>
            </user>
            """);
        XElement userNode = document.Element("user")!;
        Assert.NotNull(userNode);
        var parser = new UserDtoXmlParser();
        UserDto user = parser.Parse(userNode)!;
        Assert.NotNull(user);
        Assert.Equal("Иванов Иван Иванович", user.FullName);
        Assert.Equal("abc@email.com", user.Email);
    }
}
