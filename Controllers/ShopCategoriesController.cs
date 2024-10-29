using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Controllers
{
    public class ShopCategoriesController : Controller
    {
        private readonly IShopCategoryRepository shopCategoryRepository;

        public ShopCategoriesController(IShopCategoryRepository shopCategoryRepository)
        {
            this.shopCategoryRepository = shopCategoryRepository;
        }
        /*===========================================*/
        /*===========================================*/
        [HttpGet]
        public ActionResult AddShopCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddShopCategory(ShopCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var category = new ShopCategory()
                {
                    Name = model.Name
                };
                shopCategoryRepository.Insert(category);
                TempData["Added"] = "Category was Added.";
                return RedirectToAction();
            }
            return View(model);
        }
        /*=================================================*/
        public ActionResult DeleteShopCategory(int id)
        {
            shopCategoryRepository.Delete(id);
            return RedirectToAction("DisplayShopCategories");
        }
        /*=================================================*/
        public ActionResult DisplayShopCategories()
        {
            var categories = shopCategoryRepository.GetAll();
            return View(categories);
        }
        /*=================================================*/
    }
}
