using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class Requirement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CPU { get; set; }
        [Required]
        public int RAM { get; set; }
        [Required]
        public string GPU { get; set; }
        [Required]
        public string OS { get; set; }
        [Required]
        public int DriveSize { get; set; }
        [Required]
        public RequirementType RequirementType { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }

    public enum RequirementType
    {
        Minimal,
        Recommended
    }
}
