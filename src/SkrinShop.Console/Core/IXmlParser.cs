using System.Xml.Linq;

namespace SkrinShop.Console.Core;
public interface IXmlParser<T>
{
    T? Parse(XElement element);
}
