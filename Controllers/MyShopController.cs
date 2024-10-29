using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace AhmedStore.Controllers
{
    [Authorize(Roles = "ShopOwner")]
    public class MyShopController : Controller
    {
        private readonly IMyShopRepository myShopRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MyShopController(IMyShopRepository myShopRepository,IWebHostEnvironment webHostEnvironment)
        {
            this.myShopRepository = myShopRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("MyProducts");
        }
        public IActionResult MyProducts()
        {
            var Products = myShopRepository.GetProducts(User.Identity.Name);
            return View(Products);
        }
        /*********************************** AddProduct ***************************/
        [HttpGet]
        public IActionResult AddProduct()
        {
            var Cookie = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1)
            };
            var ShopName = myShopRepository.ShopName(User.Identity.Name);
            var ShopImageName = myShopRepository.ReturnShopImageName(User.Identity.Name);
            //Response.Cookies.Append("ShopName", $"{ShopName}");
            Response.Cookies.Append("ShopImageName", $"{ShopImageName}");
            var Products = myShopRepository.GetProducts(User.Identity.Name);
            ViewBag.Products = Products;
            MyShopAddProductsVM model = new() {
                ProductCategoryList = myShopRepository.ProductCategoryList(User.Identity.Name) };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(MyShopAddProductsVM model)
        {
            /****************************************/
            /****************************************/
            if (ModelState.IsValid)
            {
                string[] images = { "" };
                int i = 0;
                foreach (var image in model.ProductImages)
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
                myShopRepository.AddProduct(User.Identity.Name, model,images);
                return RedirectToAction();
            }
            model.ProductCategoryList = myShopRepository.ProductCategoryList(User.Identity.Name);
            var Products = myShopRepository.GetProducts(User.Identity.Name);
            ViewBag.Products = Products;
            return View(model);
        }
        /*======================= MyCategories ========================*/
        public IActionResult MyCategories()
        {
            var MyCategories = myShopRepository.MyCategories(User.Identity.Name);
            return View(MyCategories);
        }
        [HttpGet]
        public IActionResult AddMyCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMyCategory(ProductCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    ShopId = model.ShopId,
                };
                myShopRepository.AddMyCategory(User.Identity.Name, category);
                TempData["Added"] = "The Category successfully Added.";
                return RedirectToAction();
            }
            return View(model);
        }
        /*===============================================*/
        public IActionResult Delete(int id)
        {
            myShopRepository.Delete(id);
            return RedirectToAction("AddProduct");
        }
        /*==================== Delete Product Category =================*/
        public IActionResult DeleteProductCategory(int id)
        {
            bool res = myShopRepository.DeleteProductCategory(id);
            if (!res)
            {
                string message = "There are a pruducts in this category remove it first.";
                TempData["message"] = message;
                return RedirectToAction("MyCategories");
            }
            return RedirectToAction("MyCategories");

        }
        /*==================== MyOrders =================*/
        public IActionResult MyOrders()
        {
            var OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var Orders = myShopRepository.MyOrders(OwnerId);
            return View(Orders);
        }
        /*===================== Mark Status =======================*/
        public IActionResult MarkStatus(int id,string status)
        {
            myShopRepository.MarkStatus(id, status);
            return RedirectToAction("MyOrders");
        }
    }
}
