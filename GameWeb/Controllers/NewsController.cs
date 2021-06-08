using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
using GameWeb.Utilities;
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
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public NewsController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            webHostEnvironment = hostEnvironment;
            this.userManager = userManager;
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult Index()
        {
            IEnumerable<News> objList = _db.News;

            foreach (var news in objList)
            {
                if (news.AuthorID != null)
                    news.Author = _db.ApplicationUser.Find(news.AuthorID);
            }

            objList = objList.OrderByDescending(n => n.PublicationDate).ToList();

            return View(objList.ToList());
        }

        public IActionResult Display(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _db.News.Find(id);

            if (obj.AuthorID != null)
            {
                obj.Author = _db.ApplicationUser.Find(obj.AuthorID);
            }

            ViewData["Title"] = obj.Title;
            return View("Display", obj);
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public async Task<IActionResult> Create(NewsCreateViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(obj);
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                News news = new News
                {
                    Title = obj.Title,
                    Content = obj.Content,
                    PublicationDate = DateTime.Now,
                    AuthorID = user.Id,
                    Tags = obj.Tags,
                    ImagePath = uniqueFileName,
                };

                _db.News.Add(news);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        private string UploadedFile(NewsCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images", "NewsImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
