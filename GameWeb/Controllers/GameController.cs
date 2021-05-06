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

        public IActionResult Index(string SearchString)
        {
            IEnumerable<Game> objList = _db.Game;

            if (!String.IsNullOrEmpty(SearchString))
            {
                objList = objList.Where(s => s.Name.Contains(SearchString));
            }

            return View(objList.ToList());
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
                    Image = uniqueFileName,
                };

                _db.Game.Add(game);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
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
    }
}
