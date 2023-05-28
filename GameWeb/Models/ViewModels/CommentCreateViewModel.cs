using System.Collections.Generic;

namespace GameWeb.Models.ViewModels
{
    public class CommentCreateViewModel
    {
        public int ThreadId { get; set; }
        public IEnumerable<GameComment> Comments { get; set; }
        public GameComment NewComment { get; set; }

    }
}
