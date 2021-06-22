using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace BusinessLogic.Models
{
    [Table("Budget")]
    public partial class Budget
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("entrees", TypeName = "decimal(19, 5)")]
        [Display(Name = "Somme (Ar)")]
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal Entrees { get; set; }
        [Column("date_entree", TypeName = "datetime")]
        [Display(Name = "Date de saisie")]
        public DateTime? DateEntree { get; set; }
    }
}
