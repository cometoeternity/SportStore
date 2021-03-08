using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using SportStore.Controllers;
using SportStore.Models;
using System.Linq;
using SportStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace SportStore.Tests
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Paginate()
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

            ProductListViewModel result = controller.List(null,2).ViewData.Model as ProductListViewModel;

            Product[] productArray = result.Products.ToArray();
            Assert.True(productArray.Length == 2);
            Assert.Equal("Test4", productArray[0].Name);
            Assert.Equal("Test5", productArray[1].Name);
        }
        [Fact]
        public void Can_Send_Pagination_View_Model ()
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
            ProductListViewModel result = controller.List(null,2).ViewData.Model as ProductListViewModel;

            //A
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
        [Fact]
        public void Can_Filter_Product()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1 , Name = "Test1", Category = "Cat1"},
                new Product {ProductId = 2 , Name = "Test2", Category = "Cat2"},
                new Product {ProductId = 3 , Name = "Test3", Category = "Cat1"},
                new Product {ProductId = 4 , Name = "Test4", Category = "Cat2"},
                new Product {ProductId = 5 , Name = "Test5", Category = "Cat3"}
            }).AsQueryable<Product>());
            ProductController controller = new ProductController(mock.Object);
            controller.pageSize = 3;
            //A
            Product[] result = (controller.List("Cat2", 1).ViewData.Model as ProductListViewModel).Products.ToArray();
            //A
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "Test2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "Test4" && result[1].Category == "Cat2");
        }
        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            //A
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
                {
                new Product {ProductId = 1 , Name = "Test1", Category = "Cat1"},
                new Product {ProductId = 2 , Name = "Test2", Category = "Cat2"},
                new Product {ProductId = 3 , Name = "Test3", Category = "Cat1"},
                new Product {ProductId = 4 , Name = "Test4", Category = "Cat2"},
                new Product {ProductId = 5 , Name = "Test5", Category = "Cat3"}
                }).AsQueryable<Product>());
            ProductController target = new ProductController(mock.Object);
            target.pageSize = 3;
            Func<ViewResult, ProductListViewModel> GetModel = result => result?.ViewData?.Model as ProductListViewModel;

            //A
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            //A
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
