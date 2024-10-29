using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly IProductCategoryRepository ProductCategory;

        public ProductCategoriesController(IProductCategoryRepository ProductCategory)
        {
            this.ProductCategory = ProductCategory;
        }
        /*=============================================================*/
        [HttpGet]
        public ActionResult AddProductCategory() 
        {
            var model = new ProductCategoryVM
            {
                ShopList = ProductCategory.ShopList()
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult AddProductCategory(ProductCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var Category = new Category()
                {
                    Name = model.Name,
                    ShopId = model.ShopId
                };
                ProductCategory.Insert(Category);
                TempData["Added"] = "Category was Added.";
                return RedirectToAction();
            }
            return View(model);
        }
        /*=================================================*/
        public ActionResult DeleteProductCategory(int id)
        {
            ProductCategory.Delete(id);
            return RedirectToAction("DisplayProductCategories");
        }
        /*=================================================*/
        public ActionResult DisplayProductCategories() 
        {
            var categories = ProductCategory.GetAll();
            return View(categories);
        }
        /*=================================================*/
    }
}
