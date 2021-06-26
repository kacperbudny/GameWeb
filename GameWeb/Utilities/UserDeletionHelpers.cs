using GameWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Utilities
{
    public static class UserDeletionHelpers
    {
        public static void DeleteUsersContent(string userId, ApplicationDbContext _db)
        {
            DeleteUsersComments(userId, _db);
            DeleteUsersNews(userId, _db);
        }
        public static void DeleteUsersComments(string userId, ApplicationDbContext _db)
        {
            var userComments = _db.GameComment.Where(comment => comment.AuthorID == userId);

            foreach (var comment in userComments)
            {
                comment.AuthorID = null;
            }
        }
        public static void DeleteUsersNews(string userId, ApplicationDbContext _db)
        {
            var news = _db.News.Where(n => n.AuthorID == userId);

            foreach (var n in news)
            {
                n.AuthorID = null;
            }
        }
    }
}
