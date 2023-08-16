﻿// <auto-generated />
using System;
using KnowIT_TransportIT_webapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KnowIT_TransportIT_webapp.Migrations.Tickets
{
    [DbContext(typeof(TicketsContext))]
    partial class TicketsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("KnowIT_TransportIT_webapp.Models.TicketsClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Image_path")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("REAL");

                    b.Property<bool?>("Ticket_available")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ticket_description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ticket_number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Trip_title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TicketsClass");
                });
#pragma warning restore 612, 618
        }
    }
}
