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

        public IActionResult Index(int? gameId)
        {
            if(gameId == null)
            {
                return NotFound();
            }

            var threads = _db.GameCommentThread.Where(thread => thread.GameId == gameId).ToList();

            foreach (var thread in threads)
            {
                thread.Game = _db.Game.Find(gameId);
                thread.Comments = _db.GameComment.Where(comment => comment.ThreadId == thread.Id).ToList();
                thread.Comments.FirstOrDefault().Author = _db.ApplicationUser.Find(thread.Comments.FirstOrDefault().AuthorID);
            }

            threads = threads.OrderByDescending(thread => thread.Comments.FirstOrDefault().Date).ToList();

            ViewData["Title"] = "Forum gry " + threads.FirstOrDefault().Game.Name;

            return View(threads);
        }

        public IActionResult Thread(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var comments = _db.GameComment.Where(c => c.ThreadId == id);

            foreach (var comment in comments)
            {
                comment.Author = _db.ApplicationUser.Find(comment.AuthorID);
            }

            CommentCreateViewModel obj = new CommentCreateViewModel
            {
                Comments = comments,
                Thread = _db.GameCommentThread.Find(id),
            };

            obj.Thread.Game = _db.Game.Find(obj.Thread.GameId);

            ViewData["Title"] = comments.FirstOrDefault().Thread.Name + " - " + comments.FirstOrDefault().Thread.Game.Name;

            return View(obj);
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

                return RedirectToAction("Thread", "Forum", new { id = comment.ThreadId });
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddCommentPost(CommentCreateViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);

                GameComment comment = new GameComment
                {
                    Date = DateTime.Now,
                    Body = obj.NewComment.Body,
                    AuthorID = user.Id,
                    Author = user,
                    ThreadId = obj.Thread.Id,
                };

                _db.GameComment.Add(comment);
                _db.SaveChanges();

                return RedirectToAction("Thread", "Forum", new { id = comment.ThreadId });
            }
            return View(obj);
        }
    }
}
