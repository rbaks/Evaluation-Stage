using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLogic.Models
{
    [Table("City")]
    public partial class City
    {
        public City()
        {
            RouteEndCityNavigations = new HashSet<Route>();
            RouteStartCityNavigations = new HashSet<Route>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(30)]
        public string Name { get; set; }

        [InverseProperty(nameof(Route.EndCityNavigation))]
        public virtual ICollection<Route> RouteEndCityNavigations { get; set; }
        [InverseProperty(nameof(Route.StartCityNavigation))]
        public virtual ICollection<Route> RouteStartCityNavigations { get; set; }
    }
}
