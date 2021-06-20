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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        #region GET

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

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
            var user = await _userManager.FindByNameAsync(username);

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

        [Authorize]
        public IActionResult Delete()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region POST

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var username = await _userManager.FindByEmailAsync(user.Email);

                if (username == null)
                {
                    ModelState.AddModelError(string.Empty, "Nieprawidłowe dane logowania");
                    return View();
                }

                var result = await _signInManager.PasswordSignInAsync(username, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Nieprawidłowe dane logowania");
            }

            return View(user);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Manage(ManageAccountViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);

                if (isEmailTaken(user, obj))
                {
                    ModelState.AddModelError(string.Empty, "Istnieje już użytkownik o takim adresie email");
                    return View(obj);
                }

                if (obj.Email != user.Email || obj.NewPassword != null)
                {
                    if (obj.CurrentPassword == null)
                    {
                        ModelState.AddModelError(string.Empty, "Musisz zatwierdzić zmiany hasłem");
                        return View(obj);
                    }

                    var passwordValidator = new PasswordValidator<ApplicationUser>();
                    var passwordCheckResult = await passwordValidator.ValidateAsync(_userManager, null, obj.CurrentPassword);

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
                    var passwordValidators = _userManager.PasswordValidators;

                    foreach (var validator in passwordValidators)
                    {
                        var result = await validator.ValidateAsync(_userManager, null, obj.NewPassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }

                            return View(obj);
                        }
                    }

                    if (obj.NewPassword != obj.ConfirmNewPassword)
                    {
                        ModelState.AddModelError(string.Empty, "Hasła się nie zgadzają");
                        return View(obj);
                    }

                    await _userManager.ChangePasswordAsync(user, obj.CurrentPassword, obj.NewPassword);
                }

                await _userManager.UpdateAsync(user);
                _db.SaveChanges();

                return View();
            }

            return View(obj);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string? password)
        {
            if (password == null)
            {
                ModelState.AddModelError(string.Empty, "Musisz wprowadzić hasło");
                return View();
            }

            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Nie znaleziono użytkownika");
                return View();
            }

            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passwordCheckResult = await passwordValidator.ValidateAsync(_userManager, null, password);

            if (!passwordCheckResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Błędne hasło");
                return View();
            }

            deleteUsersContent(user.Id);

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region helperMethods

        public bool isEmailTaken(ApplicationUser user, ManageAccountViewModel obj)
        {
            var userFromDb = _db.ApplicationUser.FirstOrDefault(u => u.NormalizedEmail == obj.Email.ToUpper());

            if (userFromDb != null && userFromDb.Id != user.Id)
            {
                return true;
            }

            return false;
        }

        #endregion

        #region helperMethods

        private void deleteUsersContent(string userId)
        {
            deleteUsersComments(userId);
        }

        private void deleteUsersComments(string userId)
        {
            var userComments = _db.GameComment.Where(comment => comment.AuthorID == userId);

            foreach (var comment in userComments)
            {
                comment.AuthorID = null;
            }
        }

        private void deleteUsersNews(string userId)
        {
            var news = _db.News.Where(n => n.AuthorID == userId);

            foreach (var n in news)
            {
                n.AuthorID = null;
            }
        }

        #endregion
    }
}
