using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Route
{
    public class CreateRouteViewModel
    {
        [Required]
        [Display(Name = "Numéro")]
        [StringLength(20)]
        [Remote(controller: "Routes", action: "Exists")]
        public string Name { get; set; }

        [Display(Name = "Longueur (Km)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Kmlength { get; set; }
        public int StartCity { get; set; }
        public int EndCity { get; set; }

        [Display(Name = "Ville d'arrivé")]
        public virtual City EndCityNavigation { get; set; }

        [Display(Name = "Ville de départ")]
        public virtual City StartCityNavigation { get; set; }
    }
}
