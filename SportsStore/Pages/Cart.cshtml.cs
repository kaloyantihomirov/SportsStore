using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using SportsStore.Data;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    public class CartModel : PageModel
    {
        private IStoreRepository repository;

        public CartModel(IStoreRepository repo)
        {
            this.repository = repo;
        }

        public Cart? Cart { get; set; }

        public string ReturnUrl { get; set; } = "/";

        public void OnGet(string returnUrl)
        {
            this.ReturnUrl = returnUrl ?? "/";
            this.Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product? product = this.repository
                .Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                this.Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                this.Cart.AddItem(product, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }

            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}