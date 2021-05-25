using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models.ViewModels
{
    public class CommentCreateViewModel
    {
        public GameCommentThread Thread { get; set; }
        public IEnumerable<GameComment> Comments {get;set;}
        public GameComment NewComment { get; set; }

    }
}
