using GameWeb.Data;
using GameWeb.Models;
using GameWeb.ViewModels;
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

        public IActionResult Index()
        {
            IEnumerable<Game> objList = _db.Game;
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GameViewModel obj)
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

        //POST - DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Game obj)
        {
            if (ModelState.IsValid)
            {
                _db.Game.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
