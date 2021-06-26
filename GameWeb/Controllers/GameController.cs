using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
using GameWeb.Utilities;
using GameWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public GameController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        #region GET
        public IActionResult Index(string searchString)
        {
            IEnumerable<Game> objList = _db.Game;

            if (!String.IsNullOrEmpty(searchString))
            {
                objList = objList.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }

            return View(objList.ToList());
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _db.Game.Find(id);

            obj.MinimalRequirements = _db.Requirement.Find(obj.MinimalRequirementsId);
            obj.RecommendedRequirements = _db.Requirement.Find(obj.RecommendedRequirementsId);
            obj.FavouriteGames = _db.FavouriteGame.Where(fg => fg.GameId == obj.Id).ToList();
            obj.WishlistGames = _db.WishlistGame.Where(wg => wg.GameId == obj.Id).ToList();
            obj.GameRates = _db.GameRating.Where(gr => gr.GameId == obj.Id).ToList();
            obj.CommentThreads = _db.GameCommentThread.Where(thread => thread.GameId == obj.Id).ToList().TakeLast(3).Reverse().ToList();

            if (obj.GameRates.Count > 0)
            {
                obj.AverageRating = obj.GameRates.Average(game => game.Rating);
            }

            foreach (var thread in obj.CommentThreads)
            {
                thread.Comments = _db.GameComment.Where(comment => comment.ThreadId == thread.Id).ToList();

                if (thread.Comments.FirstOrDefault().AuthorID != null)
                    thread.Comments.FirstOrDefault().Author = _db.ApplicationUser.Find(thread.Comments.FirstOrDefault().AuthorID);
            }

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                obj.IsCurrentUsersFavourite = obj.FavouriteGames.Any(game => game.UserId == currentUser.Id);
                obj.IsInCurrentUsersWishlist = obj.WishlistGames.Any(game => game.UserId == currentUser.Id);

                var usersRatingObj = obj.GameRates.FirstOrDefault(game => game.UserId == currentUser.Id);

                if (usersRatingObj != null)
                {
                    obj.UserRating = usersRatingObj.Rating;
                }
            }

            ViewData["Title"] = obj.Name;
            return View("Details", obj);
        }

        [Authorize]
        public async Task<IActionResult> Favourite()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var favourites = _db.FavouriteGame.Where(fg => fg.UserId == currentUser.Id);
            IEnumerable<Game> objList = _db.Game.Where(game => favourites.Any(fav => fav.GameId == game.Id));

            return View(objList.ToList());
        }

        [Authorize]
        public async Task<IActionResult> Wishlist()
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var wishlisted = _db.WishlistGame.Where(wg => wg.UserId == currentUser.Id);
            IEnumerable<Game> objList = _db.Game.Where(game => wishlisted.Any(wl => wl.GameId == game.Id));

            return View(objList.ToList());
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Game.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.Game.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            var objViewModel = new GameEditViewModel
            {
                Id = obj.Id,
                Name = obj.Name,
                ReleaseDate = obj.ReleaseDate,
                Platform = obj.Platform,
                Publisher = obj.Publisher,
                Genre = obj.Genre,
                Description = obj.Description,
                Developer = obj.Developer,
                MinimalRequirements = _db.Requirement.Find(obj.MinimalRequirementsId),
                RecommendedRequirements = _db.Requirement.Find(obj.RecommendedRequirementsId),
                Image = obj.Image,
            };

            return View(objViewModel);
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult Create(GameCreateViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(obj);

                Game game = new Game
                {
                    Name = obj.Name,
                    ReleaseDate = obj.ReleaseDate,
                    Platform = obj.Platform,
                    Publisher = obj.Publisher,
                    Genre = obj.Genre,
                    Description = obj.Description,
                    Developer = obj.Developer,
                    MinimalRequirements = obj.MinimalRequirements,
                    MinimalRequirementsId = obj.MinimalRequirements.Id,
                    RecommendedRequirements = obj.RecommendedRequirements,
                    RecommendedRequirementsId = obj.RecommendedRequirements.Id,
                    Image = uniqueFileName,
                };

                _db.Game.Add(game);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RateGame(int id, int rating)
        {
            if (rating <= 10 && rating > 0)
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

                var newRating = new GameRating()
                {
                    GameId = id,
                    UserId = currentUser.Id,
                    Rating = rating,
                };

                var obj = _db.GameRating.Where(gr => gr.GameId == id).FirstOrDefault(gr => gr.UserId == currentUser.Id);

                if (obj != null)
                {
                    _db.GameRating.Remove(obj);
                }

                _db.GameRating.Add(newRating);
                _db.SaveChanges();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FavGame(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var newFavGame = new FavouriteGame()
            {
                GameId = id,
                UserId = currentUser.Id
            };

            _db.FavouriteGame.Add(newFavGame);
            _db.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UnfavGame(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var obj = _db.FavouriteGame.Find(id, currentUser.Id);

            _db.FavouriteGame.Remove(obj);
            _db.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> WishlistAddGame(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var newWishlistGame = new WishlistGame()
            {
                GameId = id,
                UserId = currentUser.Id
            };

            _db.WishlistGame.Add(newWishlistGame);
            _db.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> WishlistDeleteGame(int id)
        {
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var obj = _db.WishlistGame.Find(id, currentUser.Id);

            _db.WishlistGame.Remove(obj);
            _db.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Game.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            _db.Game.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult Edit(GameEditViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string fileName;

                if (obj.ImageFile != null) fileName = UploadedFile(obj);
                else fileName = obj.Image;

                Game game = new Game
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    ReleaseDate = obj.ReleaseDate,
                    Platform = obj.Platform,
                    Publisher = obj.Publisher,
                    Genre = obj.Genre,
                    Description = obj.Description,
                    Developer = obj.Developer,
                    MinimalRequirements = obj.MinimalRequirements,
                    MinimalRequirementsId = obj.MinimalRequirements.Id,
                    RecommendedRequirements = obj.RecommendedRequirements,
                    RecommendedRequirementsId = obj.RecommendedRequirements.Id,
                    Image = fileName,
                };

                _db.Game.Update(game);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #endregion

        #region helperMethods
        private string UploadedFile(GameViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "GameCovers");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        #endregion
    }
}
