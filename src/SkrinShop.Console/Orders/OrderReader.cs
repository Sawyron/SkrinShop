using Microsoft.Extensions.Logging;
using SkrinShop.Console.Core;
using System.Xml.Linq;

namespace SkrinShop.Console.Orders;

public class OrderReader
{
    private readonly IXmlParser<OrderDto> _orderParser;
    private readonly ILogger<OrderReader> _logger;

    public OrderReader(IXmlParser<OrderDto> orderParser, ILogger<OrderReader> logger)
    {
        _orderParser = orderParser;
        _logger = logger;
    }

    public IEnumerable<OrderDto> ReadOrders(Stream stream)
    {
        XDocument document = XDocument.Load(stream);
        XElement? ordersNode = document.Element("orders");
        if (ordersNode is null)
        {
            _logger.LogWarning("Orders xml node is empty. ({})", document);
            yield break;
        }
        foreach (XElement orderNode in ordersNode.Elements("order"))
        {
            OrderDto? order = _orderParser.Parse(orderNode);
            if (order is not null)
            {
                yield return order;
            }
            else
            {
                _logger.LogWarning("Can not parse order from node. ({})", orderNode);
            }
        }
    }
}
