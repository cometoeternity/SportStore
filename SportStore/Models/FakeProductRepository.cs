using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeProductRepository /*: IProductRepository*/
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Мяч", Price = 25 },
            new Product { Name = "Доска для сёрфинга", Price = 179},
            new Product { Name = "Беговые кросовки", Price = 95},
        }.AsQueryable<Product>();
    }
}
