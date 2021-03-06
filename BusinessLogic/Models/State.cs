using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models
{

    [Table("State")]
    public partial class State
    {
        public State()
        {
            Portions = new HashSet<Portion>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("label")]
        [StringLength(20)]
        [Display(Name = "Libellé")]
        public string Label { get; set; }

        [Column("perKmCost", TypeName = "decimal(19, 2)")]
        [Display(Name = "Prix de Maintenance (Ar/Km)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal PerKmCost { get; set; }

        [Display(Name = "Durée de Maintenance (/Km)")]
        [DisplayFormat(DataFormatString = "{0:N} semaines")]
        [Column("perKmDuration", TypeName = "decimal(19, 2)")]
        public decimal PerKmDuration { get; set; }

        [Display(Name = "Coefficient de dégradation (%)")]
        [DisplayFormat(DataFormatString = "{0} %")]
        [Column("coeffDeg", TypeName = "decimal(10, 2)")]
        [Range(0, 100, ErrorMessage = "le coefficient doit être positif")]
        public decimal CoeffDeg { get; set; }

        [InverseProperty(nameof(Portion.State))]
        public virtual ICollection<Portion> Portions { get; set; }
    }
}