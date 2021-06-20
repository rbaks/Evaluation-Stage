using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public double PerKmMaintenanceCost { get; set; }
        [Required]
        public double PerKmMaintenanceDuration { get; set; }
    }
}