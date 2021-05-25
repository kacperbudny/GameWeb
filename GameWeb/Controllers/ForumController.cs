using GameWeb.Data;
using GameWeb.Models;
using GameWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> userManager;

        public ForumController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create(int gameId)
        {
            ThreadCreateViewModel thread = new() { GameId = gameId, Game = _db.Game.Find(gameId) };

            return View(thread);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ThreadCreateViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                GameCommentThread thread = new GameCommentThread
                {
                    Name = obj.Name,
                    GameId = obj.GameId,
                    Game = _db.Game.Find(obj.GameId),
                };

                GameComment comment = new GameComment
                {
                    Date = DateTime.Now,
                    Body = obj.Comment.Body,
                    AuthorID = user.Id,
                    Author = user,
                    ThreadId = thread.Id,
                    Thread = thread
                };

                _db.GameComment.Add(comment);
                _db.SaveChanges();
                return RedirectToAction("Index", "Game");
            }
            return View(obj);
        }
    }
}
