using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models
{
    public class Requirement
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić procesor.")]
        [DisplayName("Procesor (oddzielać ukośnikiem)")]
        public string CPU { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić RAM.")]
        [DisplayName("RAM w GB")]
        public double RAM { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić kartę graficzną.")]
        [DisplayName("Karta graficzna")]
        public string GPU { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić system operacyjny.")]
        [DisplayName("System operacyjny")]
        public string OS { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić miejsce na dysku.")]
        [DisplayName("Miejsce na dysku w MB")]
        public int DriveSize { get; set; }
        public Game? Game { get; set; }
        public int? GameId { get; set; }
    }
}
