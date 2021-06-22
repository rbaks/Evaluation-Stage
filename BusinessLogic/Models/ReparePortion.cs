using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLogic.Models
{
    [Table("Repareportion")]
    public partial class ReparePortion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("date_rep", TypeName = "datetime")]
        [Display(Name = "Date de reparation")]
        public DateTime? DateRep { get; set; }

        [Column("portion_id")]
        public int PortionId { get; set; }

        [Column("durree_reparation", TypeName = "DECIMAL(19, 5)")]
        [DisplayFormat(DataFormatString = "{0:N} semaines")]
        [Display(Name = "Durrée de reparation")]
        public decimal DureeReparation { get; set; }

        [Column("prix_reparation", TypeName = "DECIMAL(19, 5)")]
        [DisplayFormat(DataFormatString = "{0:N} Ar")]
        [Display(Name = "Prix de reparation")]
        public decimal PrixReparation { get; set; }

        [ForeignKey(nameof(PortionId))]
        [InverseProperty("ReparePortions")]
        public virtual Portion Portion { get; set; }
    }
}
