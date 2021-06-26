using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
using GameWeb.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersManagementController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        #region GET
        public async Task<IActionResult> Index()
        {
            var list = _userManager.Users;

            foreach (var user in list)
            {
                var role = await _userManager.GetRolesAsync(user);
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

        public async Task<IActionResult> Edit(string? id)
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
                RoleList = RolesList()
            };

            var userRole = await _userManager.GetRolesAsync(obj);

            if (userRole.Count == 0)
            {
                objViewModel.SelectedRole = "0";
            }
            else
            {
                objViewModel.SelectedRole = userRole[0];
            }

            return View(objViewModel);
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

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

                var result = await _userManager.DeleteAsync(user);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(obj.Id);

                if (_db.ApplicationUser.Any(u => u.NormalizedUserName == obj.UserName.ToUpper()) && _db.ApplicationUser.FirstOrDefault(u => u.NormalizedUserName == obj.UserName.ToUpper()).Id != obj.Id)
                {
                    ModelState.AddModelError(string.Empty, "Istnieje już użytkownik o takiej nazwie");
                    obj.RoleList = RolesList();
                    return View(obj);
                }

                if (_db.ApplicationUser.Any(u => u.NormalizedEmail == obj.Email.ToUpper()) && _db.ApplicationUser.FirstOrDefault(u => u.NormalizedEmail == obj.Email.ToUpper()).Id != obj.Id)
                {
                    ModelState.AddModelError(string.Empty, "Istnieje już użytkownik o takim adresie email");
                    obj.RoleList = RolesList();
                    return View(obj);
                }

                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.BirthDate = obj.BirthDate;
                user.Description = obj.Description;

                if (obj.Password != null)
                {
                    List<string> errors = new List<string>();

                    var passwordValidators = _userManager.PasswordValidators;

                    foreach (var validator in passwordValidators)
                    {
                        var result = await validator.ValidateAsync(_userManager, null, obj.Password);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                errors.Add(error.Description);
                            }
                        }
                    }

                    if (errors.Count > 0)
                    {
                        var message = String.Join(" ", errors.ToArray());
                        ModelState.AddModelError(string.Empty, message);
                        obj.RoleList = RolesList();
                        return View(obj);
                    }

                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, obj.Password);
                }

                var userRole = await _userManager.GetRolesAsync(user);

                if (userRole.Count > 0)
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole[0]);
                }

                if (obj.SelectedRole != "0")
                {
                    await _userManager.AddToRoleAsync(user, obj.SelectedRole);
                }

                await _userManager.UpdateAsync(user);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #endregion

        #region helperMethods
        private List<SelectListItem> RolesList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Brak roli",
                    Value = "0"},
                new SelectListItem
                {
                    Text = RoleNames.GamePublisherRole,
                    Value = RoleNames.GamePublisherRole
                },
                new SelectListItem
                {
                    Text = RoleNames.NewsCreatorRole,
                    Value = RoleNames.NewsCreatorRole
                },
                new SelectListItem
                {
                    Text = RoleNames.AdminRole,
                    Value = RoleNames.AdminRole
                },
            };
        }

        #endregion
    }
}
