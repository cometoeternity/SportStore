using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System.Linq;

namespace SportStore.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId)
        {
            return View(repository.Products.FirstOrDefault(p => p.ProductId == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} был сохранен.";
                return RedirectToAction("Index");
            }
            else 
            {
                return View(product);
            }
        }
        public IActionResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} был удален!";
            }
            return RedirectToAction("Index");
        }
    }
}
