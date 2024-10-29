using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AhmedStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopRepository shopRepository;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ShopController(IShopRepository shopRepository,
            UserManager<User> userManager,IWebHostEnvironment webHostEnvironment)
        {
            this.shopRepository = shopRepository;
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }
        /*============================================*/
        /*====================== Add Shop ======================*/
        [HttpGet]
        public ActionResult AddShop()
        {
            var model = new AddShopVM() { ShopCategories = shopRepository.CategoriesSelectListItem() };
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> AddShopAsync(AddShopVM model)
        {
            if (!ModelState.IsValid)
            {
                // If the model state is invalid, repopulate the ShopCategories list
                model.ShopCategories = shopRepository.CategoriesSelectListItem();
                return View(model);  
            }

            var shop = new Shop()
            {
                Name = model.Name,
                Description = model.Description,
                ShopAddress = model.Address,
                ShopCategoryId = model.ShopCategoryId,
            };
            string uniqueFileName = "person.jpg";
            if (model.ShopImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets/img/shops");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ShopImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ShopImage.CopyToAsync(fileStream);
                }
            }
            shop.ShopImage = uniqueFileName;

            shopRepository.Insert(shop);
            TempData["Added"] = "Shop has been added.";
            return RedirectToAction("DisplayShops");
        }

        /*=================================================*/
        public ActionResult DeleteShop(int id)
        {
            shopRepository.Delete(id);
            return RedirectToAction("DisplayShops");
        }
        /*========================== DisplayShops =======================*/
        public ActionResult DisplayShops()
        {
            var shops = shopRepository.ShopWithCategoryandOwner();
            return View(shops);
        }
        /*========================== AssignShop =======================*/
        [HttpGet]
        public IActionResult AssignShop(string id)
        {
            var model = new AssignShopVM { ShopList = shopRepository.ShopListItem(),
                                            ShopOwnerId =id,
                                            
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AssignShop(AssignShopVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ShopList = shopRepository.ShopListItem();
                return View(model);
            }

            var shop = shopRepository.GetById(model.ShopId);
            shop.ShopOwnerId = model.ShopOwnerId;
            shopRepository.Edit(model.ShopId, shop);
            var user = await userManager.FindByIdAsync(model.ShopOwnerId);
            await userManager.AddToRoleAsync(user, "ShopOwner");
            return RedirectToAction("DisplayUsers","Account");
        }


        public IActionResult Details(int id)
        {
            var shop = shopRepository.Details(id);
            return View(shop);
        } 
    }
}
