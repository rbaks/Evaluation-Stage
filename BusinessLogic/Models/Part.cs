using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    public class Part
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Start { get; set; }
        [Required]
        public double End { get; set; }
        [Required]
        public State State { get; set; }
    }
}