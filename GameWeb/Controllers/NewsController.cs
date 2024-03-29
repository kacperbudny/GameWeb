﻿using GameWeb.Data;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewsController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _webHostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        #region GET

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

        public IActionResult Tag(string tagName)
        {
            if (tagName == null)
            {
                return NotFound();
            }

            IEnumerable<News> objList = _db.News.Where(n => n.Tags != null);

            foreach (var obj in objList)
            {
                if (obj.Tags != null)
                {
                    var tagsList = new List<string>();
                    obj.TagsList = new List<string>();

                    var tags = obj.Tags.Split(",");

                    foreach (var tag in tags)
                    {
                        tagsList.Add(tag.Trim());
                    }

                    obj.TagsList = tagsList;
                }
            }

            objList = objList.Where(n => n.TagsList.Contains(tagName));

            foreach (var news in objList)
            {
                if (news.AuthorID != null)
                    news.Author = _db.ApplicationUser.Find(news.AuthorID);
            }

            objList = objList.OrderByDescending(n => n.PublicationDate).ToList();

            ViewData["Title"] = "Newsy o #" + tagName;
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

            var tagsList = new List<string>();

            if (obj.Tags != null)
            {
                obj.TagsList = new List<string>();

                var tags = obj.Tags.Split(",");

                foreach (var tag in tags)
                {
                    tagsList.Add(tag.Trim());
                }

                obj.TagsList = tagsList;
            }

            ViewData["Title"] = obj.Title;
            return View("Display", obj);
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _db.News.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            var objViewModel = new NewsEditViewModel
            {
                Id = obj.Id,
                Title = obj.Title,
                Content = obj.Content,
                PublicationDate = obj.PublicationDate,
                AuthorID = obj.AuthorID,
                Tags = obj.Tags,
                Image = obj.ImagePath,
            };

            return View(objViewModel);
        }

        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.News.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        #endregion

        #region POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public async Task<IActionResult> Create(NewsCreateViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = FileUploadHelpers.UploadedFile(_webHostEnvironment, obj.ImageFile, "NewsImages");
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult Edit(NewsEditViewModel obj)
        {
            if (ModelState.IsValid)
            {
                string fileName;

                if (obj.ImageFile != null) fileName = FileUploadHelpers.UploadedFile(_webHostEnvironment, obj.ImageFile, "NewsImages");
                else fileName = obj.Image;

                News news = new News
                {
                    Id = obj.Id,
                    Title = obj.Title,
                    Content = obj.Content,
                    PublicationDate = obj.PublicationDate,
                    AuthorID = obj.AuthorID,
                    Tags = obj.Tags,
                    ImagePath = fileName,
                };

                _db.News.Update(news);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleNames.AdminRole + "," + RoleNames.NewsCreatorRole)]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.News.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.News.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}
