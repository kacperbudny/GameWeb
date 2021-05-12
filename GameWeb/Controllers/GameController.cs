using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
using GameWeb.Utilities;
using GameWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public GameController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index(string SearchString)
        {
            IEnumerable<Game> objList = _db.Game;

            if (!String.IsNullOrEmpty(SearchString))
            {
                objList = objList.Where(s => s.Name.ToLower().Contains(SearchString.ToLower()));
            }

            return View(objList.ToList());
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.GamePublisherRole)]
        public IActionResult Create()
        {
            return View();
        }

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

        public IActionResult Details(int id)
        {
            var obj = _db.Game.Find(id);
            if (obj.MinimalRequirements == null) obj.MinimalRequirements = _db.Requirement.Find(obj.MinimalRequirementsId);
            if (obj.RecommendedRequirements == null) obj.RecommendedRequirements = _db.Requirement.Find(obj.RecommendedRequirementsId);
            ViewData["Title"] = obj.Name;
            return View("Details", obj);
        }

        private string UploadedFile(GameViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "GameCovers");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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
    }
}
