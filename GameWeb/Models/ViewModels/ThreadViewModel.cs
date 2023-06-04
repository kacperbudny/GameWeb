using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;

namespace GameWeb.Models.ViewModels
{
    public class ThreadViewModel
    {
        public GameCommentThread Thread { get; set; }
        public IEnumerable<GameComment> Comments { get; set; }
        public GameComment NewComment { get; set; }
    }
}
