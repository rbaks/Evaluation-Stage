using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLogic.Models
{
    [Table("Reparation")]
    public partial class Reparation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("route_id")]
        public int RouteId { get; set; }
        [Column("date_rep", TypeName = "datetime")]
        [Display(Name = "Date de reparation")]
        public DateTime? DateRep { get; set; }

        [Column("durree_reparation", TypeName = "DECIMAL(19, 5)")]
        [Range(0, int.MaxValue)]
        public decimal DureeReparation { get; set; }

        [Column("prix_reparation", TypeName = "DECIMAL(19, 5)")]
        [Range(0, int.MaxValue)]
        public decimal PrixReparation { get; set; }

        [ForeignKey(nameof(RouteId))]
        [InverseProperty("Reparations")]
        public virtual Route Route { get; set; }
    }
}
