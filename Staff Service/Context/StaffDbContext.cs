﻿using Microsoft.EntityFrameworkCore;
using Staff_Service.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Service.Context
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext() { }
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options) { }
        public virtual DbSet<StaffDomainModel> StaffTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("StaffSchema");

            builder.Entity<StaffDomainModel>()
                .HasData(
                new StaffDomainModel
                {
                    StaffID = 1,
                    StaffFirstName = "Jacob",
                    StaffLastName = "Jardine",
                    StaffEmailAddress = "Jacob-Jardine@ThAmCo.co.uk"
                },
                new StaffDomainModel
                {
                    StaffID = 2,
                    StaffFirstName = "Ben",
                    StaffLastName = "Souch",
                    StaffEmailAddress = "Ben-Souch@ThAmCo.co.uk"
                },
                new StaffDomainModel
                {
                    StaffID = 3,
                    StaffFirstName = "Joseph",
                    StaffLastName = "Stavers",
                    StaffEmailAddress = "Joseph-Stavers@ThAmCo.co.uk"
                });
        }
    }
}
