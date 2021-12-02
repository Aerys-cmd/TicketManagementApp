﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TicketManagementApp.Models;

namespace TicketManagementApp.Migrations
{
    [DbContext(typeof(TicketManagementDbContext))]
    partial class TicketManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TicketManagementApp.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TicketManagementApp.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("ManagerId")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TicketManagementApp.Models.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CustomerId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<short>("Difficulty")
                        .HasColumnType("smallint");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("text");

                    b.Property<short>("Priority")
                        .HasColumnType("smallint");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketManagementApp.Models.TicketDetail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("TicketId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketDetails");
                });

            modelBuilder.Entity("TicketManagementApp.Models.Employee", b =>
                {
                    b.HasOne("TicketManagementApp.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TicketManagementApp.Models.Ticket", b =>
                {
                    b.HasOne("TicketManagementApp.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("TicketManagementApp.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Customer");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TicketManagementApp.Models.TicketDetail", b =>
                {
                    b.HasOne("TicketManagementApp.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("TicketId");

                    b.Navigation("Ticket");
                });
#pragma warning restore 612, 618
        }
    }
}
