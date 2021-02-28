using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 4;
        public ProductController (IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult List(int productPage = 1)
        {
            return View(new ProductListViewModel
            {
                Products = repository.Products.OrderBy(p => p.ProductId).Skip((productPage - 1) * pageSize).Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.Products.Count()
                }
            });
        }
    }
}
