using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
using GameWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _db = db;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var username = await userManager.FindByEmailAsync(user.Email);

                if (username == null)
                {
                    ModelState.AddModelError(string.Empty, "Nieprawidłowe dane logowania");
                    return View();
                }

                var result = await signInManager.PasswordSignInAsync(username, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Nieprawidłowe dane logowania");

            }

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [Authorize]
        public async Task<IActionResult> Manage()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var username = User.Identity.Name;
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var objViewModel = new ManageAccountViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Description = user.Description,
            };

            return View(objViewModel);
            
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageAccountViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = await userManager.FindByNameAsync(username);

                if (_db.ApplicationUser.Any(u => u.NormalizedEmail == obj.Email.ToUpper()) && _db.ApplicationUser.FirstOrDefault(u => u.NormalizedEmail == obj.Email.ToUpper()).Id != user.Id)
                {
                    ModelState.AddModelError(string.Empty, "Istnieje już użytkownik o takim adresie email");
                    return View(obj);
                }

                if(obj.UserName != user.UserName || obj.Email != user.Email || obj.NewPassword != null)
                {
                    if(obj.CurrentPassword == null)
                    {
                        ModelState.AddModelError(string.Empty, "Musisz zatwierdzić zmiany hasłem");
                        return View(obj);
                    }

                    var passwordValidator = new PasswordValidator<ApplicationUser>();
                    var passwordCheckResult = await passwordValidator.ValidateAsync(userManager, null, obj.CurrentPassword);

                    if (!passwordCheckResult.Succeeded)
                    {
                        ModelState.AddModelError(string.Empty, "Wprowadzono błędne dotychczasowe hasło");
                        return View();
                    }
                }

                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.BirthDate = obj.BirthDate;
                user.Description = obj.Description;

                if (obj.NewPassword != null)
                {
                    List<string> errors = new List<string>();

                    var passwordValidators = userManager.PasswordValidators;

                    foreach (var validator in passwordValidators)
                    {
                        var result = await validator.ValidateAsync(userManager, null, obj.NewPassword);

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
                        return View(obj);
                    }

                    if(obj.NewPassword != obj.ConfirmNewPassword)
                    {
                        ModelState.AddModelError(string.Empty, "Hasła się nie zgadzają");
                        return View(obj);
                    }

                    await userManager.ChangePasswordAsync(user, obj.CurrentPassword, obj.NewPassword);
                }

                await userManager.UpdateAsync(user);
                _db.SaveChanges();
                return View();
            }
            return View(obj);
        }

    [Authorize]
        public IActionResult Delete()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string? password)
        {
            if(password==null)
            {
                ModelState.AddModelError(string.Empty, "Musisz wprowadzić hasło");
                return View();
            }

            var username = User.Identity.Name;
            var user = await userManager.FindByNameAsync(username);

            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passwordCheckResult = await passwordValidator.ValidateAsync(userManager, null, password);

            if (!passwordCheckResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Błędne hasło");
                return View();
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono użytkownika");
                return View();
            }
            else
            {
                var userComments = _db.GameComment.Where(comment => comment.AuthorID == user.Id);

                foreach(var comment in userComments)
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
                    await signInManager.SignOutAsync();
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
