using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BusinessLogic.Models
{
    public partial class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Portion> Portions { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<State> States { get; set; }
    }
}
