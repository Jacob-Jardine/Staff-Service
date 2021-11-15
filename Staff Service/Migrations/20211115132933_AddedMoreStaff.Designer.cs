﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Staff_Service.Context;

namespace Staff_Service.Migrations
{
    [DbContext(typeof(dbContext))]
    [Migration("20211115132933_AddedMoreStaff")]
    partial class AddedMoreStaff
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("staff")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Staff_Service.DomainModel.StaffDomainModel", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StaffEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffFirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffLastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StaffID");

                    b.ToTable("_staff");

                    b.HasData(
                        new
                        {
                            StaffID = 1,
                            StaffEmailAddress = "Jacob-Jardine@ThAmCo.co.uk",
                            StaffFirstName = "Jacob",
                            StaffLastName = "Jardine"
                        },
                        new
                        {
                            StaffID = 2,
                            StaffEmailAddress = "Ben-Souch@ThAmCo.co.uk",
                            StaffFirstName = "Ben",
                            StaffLastName = "Souch"
                        },
                        new
                        {
                            StaffID = 3,
                            StaffEmailAddress = "Joseph-Stavers@ThAmCo.co.uk",
                            StaffFirstName = "Joseph",
                            StaffLastName = "Stavers"
                        },
                        new
                        {
                            StaffID = 4,
                            StaffEmailAddress = "Teddy-Teasdale@ThAmCo.co.uk",
                            StaffFirstName = "Teddy",
                            StaffLastName = "Teasdale"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
