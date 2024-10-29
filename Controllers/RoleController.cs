using AhmedStore.Models;
using AhmedStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AhmedStore.Controllers
{
    public class RoleController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        /*==================== Add Role ====================*/
        [HttpGet]
        public async Task<IActionResult> AddRoleAsync()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (await roleManager.RoleExistsAsync(model.RoleName))
                {
                    TempData["Exist"] = $"The {model.RoleName} Role is already Exist";
                }
                IdentityRole Role = new()
                {
                    Name = model.RoleName
                };
                
                await roleManager.CreateAsync(Role);
                return RedirectToAction();
            }
            return View(model);
        }
        /*===================== Display Roles ===================*/
        public IActionResult DisplayRoles()
        {
            var Roles = roleManager.Roles.ToList();
            return View(Roles); 
        }
        /*====================== Delete Role ==================*/
        public async Task<IActionResult> DeleteRole(string Id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(Id);
            roleManager.DeleteAsync(role);
            return RedirectToAction("DisplayRoles");
        }
        /*====================== Assign Role ==================*/
        [HttpGet]
        public async Task<IActionResult> AssignRole(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                ViewBag.UserRoles = roles;
            }
            // Fetch roles asynchronously
            
            AssignRoleVM model = new()
            {
                RoleList = await roleManager.Roles
                    .Where(r => r.Name != "ShopOwner")
                    .Select(R => new SelectListItem
                    {
                        Text = R.Name,
                        Value = R.Id.ToString()
                    })
                    .ToListAsync(), // Ensure the query is materialized into a list
                UserId = Id
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserId);
                var role = await roleManager.FindByIdAsync(model.RoleId);
                if (user is not null && role.Name is not null)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                    TempData["Added"] = $"{user.UserName} is now {role.Name}";
                }
                return RedirectToAction();
            }
            model.RoleList = await roleManager.Roles
                    .Where(r => r.Name != "ShopOwner")
                    .Select(R => new SelectListItem
                    {
                        Text = R.Name,
                        Value = R.Id.ToString()
                    })
                    .ToListAsync(); // Ensure the query is materialized into a list
            return View(model);
        }
        /*====================== Remove Role ==================*/
        public async Task<IActionResult> RemoveRoleFromUser(string Id, string roleName)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user.NormalizedUserName != "KAREEM" || roleName != "Admin")
            {
                if (user is not null && roleName is not null)
                {
                    await userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
            return RedirectToAction("AssignRole","Role", new { Id = Id });
        }
    }
}
