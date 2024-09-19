using SkrinShop.Console.Core;
using System.Xml.Linq;

namespace SkrinShop.Console.Users;

public class UserDtoXmlParser : IXmlParser<UserDto>
{
    public UserDto? Parse(XElement element)
    {
        string? fullName = element.Element("fio")?.Value;
        string? email = element.Element("email")?.Value;
        if (fullName is null || email is null)
        {
            return null;
        }
        return new UserDto(fullName, email);
    }
}
