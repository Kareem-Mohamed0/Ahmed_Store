using AhmedStore.Data;
using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AhmedStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository productRepository;

        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var ProductsVM = productRepository.AllProducstWithOneImage();
            return View(ProductsVM);
        }

    }
}
