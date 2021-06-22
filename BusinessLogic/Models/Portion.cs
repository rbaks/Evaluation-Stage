using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models
{
    [Table("Portion")]
    public partial class Portion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(20)")]
        public string Name { get; set; }

        [Column("start_portion", TypeName = "decimal(5, 2)")]
        [Display(Name = "Debut")]
        [DisplayFormat(DataFormatString = "{0:N} Km")]
        [Range(0, int.MaxValue, ErrorMessage = "la borne doit étre positive")]
        public decimal StartPortion { get; set; }

        [Column("end_portion", TypeName = "decimal(5, 2)")]
        [Display(Name = "Fin")]
        [Range(0, int.MaxValue, ErrorMessage = "la borne doit étre positive")]
        [DisplayFormat(DataFormatString = "{0:N} Km")]
        public decimal EndPortion { get; set; }
        [Column("route_id")]
        public int RouteId { get; set; }
        [Column("state_id")]
        public int StateId { get; set; }

        [ForeignKey(nameof(RouteId))]
        [InverseProperty("Portions")]
        public virtual Route Route { get; set; }
        [ForeignKey(nameof(StateId))]
        [InverseProperty("Portions")]

        [Display(Name = "Etat")]
        public virtual State State { get; set; }

        //Modifications

        [Column("kmlength", TypeName = "decimal(5, 2)")]
        public decimal Kmlength { get; set; }
        [Column("previous")]
        public int? Previous { get; set; }

        [Display(Name = "Portion précedée")]
        [ForeignKey(nameof(Previous))]
        [InverseProperty(nameof(Portion.InversePreviousNavigation))]
        public virtual Portion PreviousNavigation { get; set; }

        [InverseProperty(nameof(Portion.PreviousNavigation))]
        public virtual ICollection<Portion> InversePreviousNavigation { get; set; }

        [InverseProperty(nameof(ReparePortion.Portion))]
        public virtual ICollection<ReparePortion> ReparePortions { get; set; }

        public decimal Length 
        { 
            get
            {
                return EndPortion - StartPortion;
            }
        }

        public decimal GetPrixReparation()
        {
            return State.PerKmCost* Length;
        }

        public decimal GetDureeReparation()
        {
            return State.PerKmDuration * Length;
        }
    }
}