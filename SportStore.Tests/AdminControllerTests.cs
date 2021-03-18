using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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


        [Fact]
        public void Can_Edit_Product()
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
            Product p1 = GetViewModel<Product>(target.Edit(1));
            Product p2 = GetViewModel<Product>(target.Edit(2));
            Product p3 = GetViewModel<Product>(target.Edit(3));

            //A
            Assert.Equal(1, p1.ProductId);
            Assert.Equal(2, p2.ProductId);
            Assert.Equal(3, p3.ProductId);
        }

        [Fact]
        public void Cannot_Edit_Nonexistent_Product()
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
            Product result = GetViewModel<Product>(target.Edit(4));

            //A
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();
            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };
            Product product = new Product { Name = "Test" };

            //A
            IActionResult result = target.Edit(product);

            //A
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product { Name = "Test" };
            target.ModelState.AddModelError("error", "error");

            //A
            IActionResult result = target.Edit(product);

            //A
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void Can_Delete_Valid_Product()
        {
            //A
            Product prod = new Product { ProductId = 2, Name = "Test2" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductId = 1, Name = "Test2"},
                prod,
                new Product { ProductId = 3, Name = "Test3"}
            }).AsQueryable<Product>());
            AdminController target = new AdminController(mock.Object);

            //A
            target.Delete(prod.ProductId);

            //A
            mock.Verify(m => m.DeleteProduct(prod.ProductId));
        }
    }
}
