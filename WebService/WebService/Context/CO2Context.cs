using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Context
{
    public class CO2Context : DbContext
    {
        public CO2Context(DbContextOptions<CO2Reading> options) : base(options)
        {
        }

        public DbSet<CO2Reading> EmployeeList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CO2Reading>()
                .HasNoKey()
                .Property(e => e.reading)
                .IsRequired();
        }

    }
}
