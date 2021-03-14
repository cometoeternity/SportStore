using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System.Linq;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        public OrderController(IOrderRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }
        public ViewResult Checkout () => View(new Order());
        [HttpPost]
        public IActionResult Checkout (Order order)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извените, но ваша корзина пуста!");
            }
            if(ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
         public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
