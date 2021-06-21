using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessLogic.Models
{
    public partial class Etat
    {
        [Column("routeid", TypeName = "varchar(20)")]
        public string RouteId { get; set; }
        [Column("name", TypeName = "varchar(20)")]
        public string Name { get; set; }
        [Column("depart", TypeName = "varchar(20)")]
        public string Depart { get; set; }
        [Column("arrive", TypeName = "varchar(20)")]
        public string Arrive { get; set; }
        [Column("etatglobal", TypeName = "varchar(20)")]
        public string Etatglobal { get; set; }
    }
}
