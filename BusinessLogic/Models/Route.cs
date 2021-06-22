using System;
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

        [Column("kmlength", TypeName = "decimal(19, 2)")]
        [Display(Name = "Longueur (Km)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        [Range(0, int.MaxValue)]
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




        /*        public decimal GetEtatGlobal()
                {
                    foreach (Portion portion in Portions)
                    {
                        portion.State.
                    }
                    return 1;
                }*/
        [Column("etat", TypeName = "varchar(20)")]
        public string Etat { get; set; }

        public bool isValid()
        {
            Portion[] portions = new Portion[Portions.Count];
            Portions.CopyTo(portions, 0);

            if (portions.Length >= 2)
            {
                decimal totalLength = 0;

                for (int i = 0; i < portions.Length - 1; i++)
                {
                    totalLength += portions[i].Length;
                    if (portions[i].EndPortion > portions[i + 1].StartPortion) return false;
                }
                return (totalLength + portions[portions.Length - 1].Length) == Kmlength;
            }
            else if (portions.Length == 0) return false;
            else return portions[0].Length == Kmlength;
        }

        public decimal GetPrixReparation()
        {
            Portion[] portions = new Portion[Portions.Count];
            Portions.CopyTo(portions, 0);
            decimal totalPrice = 0;

            for (int i = 0; i < portions.Length - 1; i++)
            {
                totalPrice += portions[i].GetPrixReparation();
            }
            return totalPrice;
        }

        public decimal GetDureeReparation()
        {
            Portion[] portions = new Portion[Portions.Count];
            Portions.CopyTo(portions, 0);
            decimal totalDuration = 0;

            for (int i = 0; i < portions.Length - 1; i++)
            {
                totalDuration += portions[i].GetDureeReparation();
            }
            return totalDuration;
        }
    }
}
