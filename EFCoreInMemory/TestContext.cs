using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreInMemory
{
    internal class TestContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
            => optionsBuilder.UseInMemoryDatabase( "PeopleDatabase" );

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity<Person>( entity =>
            {
                entity.HasData( new[]
                {
                    new Person { Id = 1, Name = "Tommy", Age = 42 },
                    new Person { Id = 2, Name = "Luke", Age = 69 }
                } );
            });
        }

        
    }
}
