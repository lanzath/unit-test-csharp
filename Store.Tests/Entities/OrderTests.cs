using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Customer _customer;

    public OrderTests()
        => _customer = new Customer("Thiago Lanza", "lanzathm@gmail.com");

    [TestMethod]
    [TestCategory("Domain")]
    public void ItShouldGenerateNewOrderNumberWith8Character()
    {
        var order = new Order(_customer, 0, null);
        Assert.AreEqual(8, order.Number.Length);
    }

    // [TestMethod]
    // [TestCategory("Domain")]
    // public void ItShouldHaveWaitingPaymentStatusOnNewOrders()
    // {
    //     Assert.Fail();
    // }

    // [TestMethod]
    // [TestCategory("Domain")]
    // public void ItShouldHaveWaitingDeliveryStatusOnPaymentComplete()
    // {
    //     Assert.Fail();
    // }
}
