using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.productRepository = productRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        /*=======================================================*/
        [HttpGet]
        public ActionResult AddProduct()
        {
            var model = new AddProductVM() { ShopList = productRepository.ShopList(),
                                             ProductCategoryList = productRepository.ProductCategoryList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(AddProductVM model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, repopulate the ShopCategories list
                model.ShopList = productRepository.ShopList();
                model.ProductCategoryList = productRepository.ProductCategoryList();
                return View(model);  // This will render the view and display validation errors
            }

            var Product = new Product()
            {
                ProductName = model.ProductName,
                Description = model.Description,
                DiscountPercentage = model.DiscountPercentage,
                Price = model.Price,
                Stock = model.Stock,
                CategoryId = model.CategoryId,
                ShopId = model.ShopId
            };
            string[] images = { "" };
            int i = 0;
            foreach(var image in model.ProductImages)
            {
                string uniqueFileName = "";
                if (image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets/img/product");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }
                images[i] = uniqueFileName;
            }
            productRepository.Insert(Product);
            productRepository.AddImagesToProduct(Product.Id,images);
            
            TempData["Added"] = "Product has been added.";
            return RedirectToAction("Index","Home");
        }
        /*=======================================================*/
        /*========================== DisplayProducts =============================*/
        public ActionResult DisplayProducts()
        {
            var Products = productRepository.AllProductsForAdmin();
            return View(Products);
        }
        /*========================== Delete =============================*/
        public IActionResult Delete(int id)
        {
            productRepository.Delete(id);
            return RedirectToAction("DisplayProducts");
        }
        /*========================== Display Prouct Of a certain Shop For User =======================*/
    }
}
