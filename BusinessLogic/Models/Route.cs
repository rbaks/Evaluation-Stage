﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web.Mvc;

namespace BusinessLogic.Models
{
    [Table("Route")]
    public partial class Route
    {
        public Route()
        {
            Portions = new HashSet<Portion>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [Display(Name = "Numéro")]
        [StringLength(20)]
        public string Name { get; set; }
        [Column("start_city")]
        public int StartCity { get; set; }
        [Column("end_city")]
        public int EndCity { get; set; }

        [Column("kmlength", TypeName = "decimal(5, 2)")]
        [Display(Name = "Longueur (Km)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Kmlength { get; set; }

        [Display(Name = "Ville d'arrivé")]
        [ForeignKey(nameof(EndCity))]
        [InverseProperty(nameof(City.RouteEndCityNavigations))]
        public virtual City EndCityNavigation { get; set; }

        [Display(Name = "Ville de départ")]
        [ForeignKey(nameof(StartCity))]
        [InverseProperty(nameof(City.RouteStartCityNavigations))]
        public virtual City StartCityNavigation { get; set; }
        [InverseProperty(nameof(Portion.Route))]
        public virtual ICollection<Portion> Portions { get; set; }
    }
}
