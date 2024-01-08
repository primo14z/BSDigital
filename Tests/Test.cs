using BSDigital.Models;
using BSDigital.Service;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class Tests
{
    private IOrderService orderService;

    [SetUp]
    public void SetUp()
    {
        orderService = new OrderService();
    }

    [Test]
    public void OrderService_BuyOrder_ReturnsBestPrice()
    {
        var orderInput = new OrderInput("Buy", 0.2);

        // Act
        List<Order> outputOrders = orderService.GenerateOrder(orderInput);

        // Assert
        Assert.That(outputOrders.Count, Is.EqualTo(1));
        Assert.That(outputOrders[0].Type, Is.EqualTo("Buy"));
    }

    [Test]
    public void AnalyzeOrderBooks_SellOrder_ReturnsBestPrice()
    {
        OrderInput orderInput = new OrderInput("Sell", 1.0 );

        // Act
        List<Order> outputOrders = orderService.GenerateOrder(orderInput);

        // Assert
        Assert.That(outputOrders.Count, Is.EqualTo(1));
        Assert.That(outputOrders[0].Type, Is.EqualTo("Sell"));
        // Add more assertions as needed
    }
}