using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.Models
{
    public class Route
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string number { get; set; }
        [Required]
        public double KmLength { get; set; }
        [Required]
        public City Start { get; set; }
        [Required]
        public City End { get; set; }
        public List<Part> Parts { get; set; }
    }
}
