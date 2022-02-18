using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities;

[TestClass]
public class OrderTests
{
    private readonly Customer _customer = new("Thiago Lanza", "lanzathm@gmail.com");
    private readonly Product _product = new("Product 1", 10, true);
    private readonly Discount _discount = new(10, DateTime.Now.AddDays(5));

    [TestMethod, TestCategory("Domain")]
    public void ItShouldGenerateNewOrderNumberWith8Character()
    {
        var order = new Order(_customer, 0, null);
        Assert.AreEqual(8, order.Number.Length);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveWaitingPaymentStatusOnNewOrders()
    {
        var order = new Order(_customer, 0, null);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveWaitingDeliveryStatusOnPaymentComplete()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 2);
        order.Pay(20);
        Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveCanceledStatusOnOrderCancel()
    {
        var order = new Order(_customer, 0, null);
        order.Cancel();
        Assert.AreEqual(order.Status, EOrderStatus.Canceled);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldNotAddANewItemWithoutAProduct()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(null, 10);
        Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldNotAddANewItemWithQuantityEqualOrLesserThanZero()
    {
        var order = new Order(_customer, 0, null);
        order.AddItem(_product, 0);
        order.AddItem(_product, -1);
        Assert.AreEqual(order.Items.Count, 0);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveTotalValueOf50OnAValidOrder()
    {
        var order = new Order(_customer, 10, _discount); // Delivery Fee: $10, Discount: $10 -> total = 0
        order.AddItem(_product, 5); // 5 product of $10 each: $50
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveTotalValueOf60OnAnExpiredDiscount()
    {
        var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-10));
        var order = new Order(_customer, 10, expiredDiscount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveTotalValueOf60OnAnInvalidDiscount()
    {
        // Since discount has a nullable validation, it'll have 0 assigned by default
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveTotalValueOf50OnAValidDiscount()
    {
        var order = new Order(_customer, 10, _discount);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 50);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldHaveTotalValueOf60OnDeliveryFeeWas10()
    {
        var order = new Order(_customer, 10, null);
        order.AddItem(_product, 5);
        Assert.AreEqual(order.Total(), 60);
    }

    [TestMethod, TestCategory("Domain")]
    public void ItShouldBeAInvalidOrderOnCustomerAbsence()
    {
        var order = new Order(null, 10, null);
        Assert.IsFalse(order.IsValid);
    }
}
