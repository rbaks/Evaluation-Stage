﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models
{
    [Table("Portion")]
    public partial class Portion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("start_portion", TypeName = "decimal(5, 2)")]
        [Display(Name = "Debut")]
        [Range(0, int.MaxValue, ErrorMessage = "la borne doit étre positive")]
        public decimal StartPortion { get; set; }

        [Column("end_portion", TypeName = "decimal(5, 2)")]
        [Display(Name = "Fin")]
        [Range(0, int.MaxValue, ErrorMessage = "la borne doit étre positive")]
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
    }
}