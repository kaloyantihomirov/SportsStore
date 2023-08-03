using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(
            IOrderRepository repoService, 
            Cart cartService)
        {
            this.repository = repoService;
            this.cart = cartService;
        }
        public ViewResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (this.cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = this.cart.Lines.ToArray();
                this.repository.SaveOrder(order);
                this.cart.Clear();
                return RedirectToPage("/Completed", new { orderId = order.OrderID });
            }
            else
            {
                return View();
            }
        }       
    }
}
