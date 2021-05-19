using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers
{
    [Authorize(Roles = RoleNames.AdminRole)]
    public class UsersManagementController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersManagementController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = userManager.Users;

            foreach(var user in list)
            {
                var role = await userManager.GetRolesAsync(user);
                user.Role = role.FirstOrDefault();
            }

            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var username = User.Identity.Name;
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono użytkownika");
                return View();
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
