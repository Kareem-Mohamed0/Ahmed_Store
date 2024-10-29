using AhmedStore.Models;
using AhmedStore.Repository;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AhmedStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IUserRepository userRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager,
            IWebHostEnvironment webHostEnvironment, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.userRepository = userRepository;
        }
        /*=====================================================*/
        [HttpGet]
        public IActionResult Register()
        {
            RegisterVM model = new RegisterVM();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            string uniqueFileName = "person.jpg";
            if (ModelState.IsValid) 
            {
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets/img/users");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }
                }
                var User = new User()
                {
                    UserName = model.Name,
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    Image = uniqueFileName,
                    PhoneNumber = model.Phone,
                    Address = model.Address
                };
                var result =await userManager.CreateAsync(User, model.Pass);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(User,isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        /*==============================================================*/
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            ProfileVM model = new ProfileVM();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user != null) 
            {
                model.Name = user.UserName;
                model.Address = user.Address;
                model.Phone = user.PhoneNumber;
                model.BirthDate = user.BirthDate;
                model.Image = user.Image;
                model.Email = user.Email;
            }
            return View(model);
        }
        /*============================== Edit User =================================*/
        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
            ProfileVM model = new();
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                model.Name = user.UserName;
                model.Address = user.Address;
                model.Phone = user.PhoneNumber;
                model.BirthDate = user.BirthDate;
                model.Image = user.Image;
                model.Email = user.Email;
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserVM model)
        {
            string uniqueFileName = model.Image != null ? Guid.NewGuid().ToString() + "_" + model.Image.FileName : null;

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (model.Image != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "assets/img/users");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        user.Image = uniqueFileName; // Update user image only if a new one was uploaded
                    }

                    // Update other fields
                    user.PhoneNumber = model.Phone;
                    user.BirthDate = model.BirthDate;
                    user.Email = model.Email;
                    user.Address = model.Address;

                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            ProfileVM profileVM = new ProfileVM()
            {
                Name = model.Name,
                Phone = model.Phone,
                Address = model.Address,
                BirthDate = model.BirthDate,
                Email = model.Email,
                Image = model.Image != null ? model.Image.FileName : "person.jpg" // Handle null image
            };

            return View(profileVM);
        }
        /*=======================================================*/
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            /*----------*/
            if (Request.Cookies["ShopName"] != null)
            {
                Response.Cookies.Append("ShopName", "", new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(-1) // Set the expiration to a past date
                });
            }
            /*-----------*/
            return RedirectToAction("Index", "Home");
        }
        /*=======================================================*/
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, model.Password);
                    if (found)
                    {
                        await signInManager.SignInAsync(user, model.rememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "UserName Or Password is Invalid!");
                    return View(model);
                }
                ModelState.AddModelError("", "UserName Or Password is Invalid!");
            }
            return View(model);
        }
        /*================================= DisplayUsers ==============================*/
        public IActionResult DisplayUsers()
        {
            var Users = userRepository.DisplayUsers();
            return View(Users);
        }
        /*-----------------------------------------------------------------------------*/
        public async Task<IActionResult> Delete(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            await userManager.DeleteAsync(user);
            return RedirectToAction("DisplayUsers");
        }

    }
}
