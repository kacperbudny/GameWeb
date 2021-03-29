using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}