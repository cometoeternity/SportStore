using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using SportStore.Components;
using SportStore.Models;
using Microsoft.AspNetCore.Components;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1 , Name = "Test1", Category = "Apples"},
                new Product {ProductId = 2 , Name = "Test2", Category = "Apples"},
                new Product {ProductId = 3 , Name = "Test3", Category = "Plums"},
                new Product {ProductId = 4 , Name = "Test4", Category = "Oranges"}
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //A
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            //A
            Assert.True(Enumerable.SequenceEqual(new string[]
            {
                "Apples", "Oranges", "Plums"
            }, results));
        }
        [Fact]
        public void Indicated_Selected_Category()
        {
            //A
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1 , Name = "Test1", Category = "Apples"},
                new Product {ProductId = 4 , Name = "Test4", Category = "Oranges"}
            }).AsQueryable<Product>());
            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext { RouteData = new Microsoft.AspNetCore.Routing.RouteData() }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            //A
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //A
            Assert.Equal(categoryToSelect, result);
        }
    }
}
