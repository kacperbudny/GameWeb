using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class GameComment
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "Proszę wprowadzić treść komentarza.")]
        [DisplayName("Treść komentarza")]
        public string Body { get; set; }
        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }
        public int ThreadId { get; set; }
        public GameCommentThread Thread { get; set; }
    }
}
