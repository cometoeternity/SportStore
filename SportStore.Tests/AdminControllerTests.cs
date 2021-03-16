using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Controllers;
using System.Linq;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "Test1"},
                new Product { ProductId = 2, Name = "Test2"},
                new Product { ProductId = 3, Name = "Test3"}
            }).AsQueryable<Product>());
            AdminController target = new AdminController(mock.Object);

            //A
            Product[] result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            //A
            Assert.Equal(3, result.Length);
            Assert.Equal("Test1", result[0].Name);
            Assert.Equal("Test2", result[1].Name);
            Assert.Equal("Test3", result[2].Name);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
