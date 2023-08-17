﻿// <auto-generated />
using System;
using KnowIT_TransportIT_webapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KnowIT_TransportIT_webapp.Migrations.Tickets
{
    [DbContext(typeof(TicketsContext))]
    [Migration("20230816235033_InitialCreateT")]
    partial class InitialCreateT
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("KnowIT_TransportIT_webapp.Models.TicketsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("REAL");

                    b.Property<bool?>("TicketAvailable")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TicketDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TicketNumber")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TripTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("WeekDay")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TicketsClass");
                });
#pragma warning restore 612, 618
        }
    }
}