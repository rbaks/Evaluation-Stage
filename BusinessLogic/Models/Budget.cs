using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        public static decimal GetTotalBudget(List<Budget> budgets, List<ReparePortion> reparePortions) 
        {
            return budgets.Sum(b => b.Entrees) - reparePortions.Sum(r => r.PrixReparation);
        }
    }
}
