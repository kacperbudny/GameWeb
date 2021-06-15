using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
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

            foreach (var user in list)
            {
                var role = await userManager.GetRolesAsync(user);
                user.Role = role.FirstOrDefault();
            }

            return View(list);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _db.ApplicationUser.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);

            var user = _db.ApplicationUser.Find(id);

            if (id == currentUser.Id)
            {
                ModelState.AddModelError(string.Empty, "Nie możesz usunąć własnego konta w ten sposób!");
                return View("Delete", user);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono użytkownika");
                return View();
            }
            else
            {
                var userComments = _db.GameComment.Where(comment => comment.AuthorID == user.Id);

                foreach (var comment in userComments)
                {
                    comment.AuthorID = null;
                }

                var news = _db.News.Where(n => n.AuthorID == user.Id);

                foreach (var n in news)
                {
                    n.AuthorID = null;
                }

                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction("Index");
            }
        }

        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _db.ApplicationUser.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            var objViewModel = new UserEditViewModel
            {
                Id = obj.Id,
                UserName = obj.UserName,
                Email = obj.Email,
                BirthDate = obj.BirthDate,
                Description = obj.Description,
            };

            return View(objViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(obj.Id);

                if (_db.ApplicationUser.Any(u => u.NormalizedUserName == obj.UserName.ToUpper()) && _db.ApplicationUser.FirstOrDefault(u => u.NormalizedUserName == obj.UserName.ToUpper()).Id != obj.Id)
                {
                    ModelState.AddModelError(string.Empty, "Istnieje już użytkownik o takiej nazwie");
                    return View(obj);
                }

                if (_db.ApplicationUser.Any(u => u.NormalizedEmail == obj.Email.ToUpper()) && _db.ApplicationUser.FirstOrDefault(u => u.NormalizedEmail == obj.Email.ToUpper()).Id != obj.Id)
                {
                    ModelState.AddModelError(string.Empty, "Istnieje już użytkownik o takim adresie email");
                    return View(obj);
                }

                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.BirthDate = obj.BirthDate;
                user.Description = obj.Description;

                if (obj.Password != null)
                {
                    List<string> errors = new List<string>();

                    var passwordValidators = userManager.PasswordValidators;

                    foreach (var validator in passwordValidators)
                    {
                        var result = await validator.ValidateAsync(userManager, null, obj.Password);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                errors.Add(error.Description);
                            }
                        }
                    }

                    if(errors.Count > 0)
                    {
                        var message = String.Join(" ", errors.ToArray());
                        ModelState.AddModelError(string.Empty, message);
                        return View(obj);
                    }

                    await userManager.RemovePasswordAsync(user);
                    await userManager.AddPasswordAsync(user, obj.Password);
                }

                await userManager.UpdateAsync(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
