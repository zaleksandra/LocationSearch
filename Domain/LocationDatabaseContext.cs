using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class LocationDatabaseContext : DbContext
    {
        public LocationDatabaseContext(DbContextOptions<LocationDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected LocationDatabaseContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Location> Locations { get; set; }
    }
}
