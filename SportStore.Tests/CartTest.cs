using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SportStore.Models;
using System.Linq;

namespace SportStore.Tests
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Lines ()
        {
            //A
            Product p1 = new Product { ProductId = 1, Name = "Test1"};
            Product p2 = new Product { ProductId = 2, Name = "Test2" };
            Cart target = new Cart();

            //A
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] result = target.Lines.ToArray();

            //A
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //A
            Product p1 = new Product { ProductId = 1, Name = "Test1" };
            Product p2 = new Product { ProductId = 2, Name = "Test2" };
            Cart target = new Cart();

            //A
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] result = target.Lines.OrderBy(p => p.Product.ProductId).ToArray();

            //A
            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        { 
            //A
            Product p1 = new Product { ProductId = 1, Name = "Test1" };
            Product p2 = new Product { ProductId = 2, Name = "Test2" };
            Product p3 = new Product { ProductId = 3, Name = "Test3" };
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            //A
            target.RemoveLine(p2);

            //A
            Assert.Empty(target.Lines.Where(p => p.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            //A
            Product p1 = new Product { ProductId = 1, Name = "Test1", Price = 100m };
            Product p2 = new Product { ProductId = 2, Name = "Test2", Price = 50m };
            Cart target = new Cart();

            //A
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            //A
            Assert.Equal(450m, result);
        }
        [Fact]
         public void Can_Clear_Contents()
         {
             //A
             Product p1 = new Product { ProductId = 1, Name = "Test1", Price = 100m };
             Product p2 = new Product { ProductId = 2, Name = "Test2", Price = 50m };
             Cart target = new Cart();
             target.AddItem(p1, 1);
             target.AddItem(p2, 1);

            //A
            target.Clear();

            //A
            Assert.Empty(target.Lines);
         }

        
    }
}
