using System;
using System.Collections.Generic;
using System.Text;
using SportStore.Models;
using SportStore.Controllers;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace SportStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Empty_Cart()
        {
            //a
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            Order order = new Order();
            OrderController target = new OrderController(mock.Object, cart);

            //A
            ViewResult result = target.Checkout(order) as ViewResult;

            //A
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            //A
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object,cart);
            target.ModelState.AddModelError("error", "error");

            //A
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //A
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }
        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            //A
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            OrderController target = new OrderController(mock.Object, cart);

            //A
            RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

            //A
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
