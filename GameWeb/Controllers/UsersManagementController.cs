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
    }
}
