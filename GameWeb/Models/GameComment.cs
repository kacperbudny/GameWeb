using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class GameComment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "ntext")]
        public string Body { get; set; }
        public string UserID { get; set; }
        public int ThreadId { get; set; }
        public GameCommentThread Thread { get; set; }
    }
}
