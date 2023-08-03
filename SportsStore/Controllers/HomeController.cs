using Microsoft.AspNetCore.Mvc;
using SportsStore.Data;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;

        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index(string? category, int productPage = 1)
        =>  View(new ProductsListViewModel
            {
                Products = this.repository
                .Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                },
                CurrentCategory = category
            });                  
    }
}