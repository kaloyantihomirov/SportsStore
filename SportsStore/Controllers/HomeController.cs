using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;

        public HomeController(IStoreRepository repo)
        {
            this.repository = repo;
        }

        public IActionResult Index()
        {
            return View(this.repository.Products);
        }
    }
}