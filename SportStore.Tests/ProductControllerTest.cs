using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using SportStore.Controllers;
using SportStore.Models;
using System.Linq;
using SportStore.Models.ViewModels;

namespace SportStore.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void CanPaginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
                {
                new Product {ProductId = 1 , Name = "Test1"},
                new Product {ProductId = 2 , Name = "Test2"},
                new Product {ProductId = 3 , Name = "Test3"},
                new Product {ProductId = 4 , Name = "Test4"},
                new Product {ProductId = 5 , Name = "Test5"}
                }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;

            ProductListViewModel result = controller.List(2).ViewData.Model as ProductListViewModel;

            Product[] productArray = result.Products.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("Test4", productArray[0].Name);
            Assert.Equal("Test5", productArray[1].Name);
        }
        [Fact]
        public void CanSendPaginationViewModel ()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
               {
                new Product {ProductId = 1 , Name = "Test1"},
                new Product {ProductId = 2 , Name = "Test2"},
                new Product {ProductId = 3 , Name = "Test3"},
                new Product {ProductId = 4 , Name = "Test4"},
                new Product {ProductId = 5 , Name = "Test5"}
               }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object) { pageSize = 3 };

            //A
            ProductListViewModel result = controller.List(2).ViewData.Model as ProductListViewModel;

            //A
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}
