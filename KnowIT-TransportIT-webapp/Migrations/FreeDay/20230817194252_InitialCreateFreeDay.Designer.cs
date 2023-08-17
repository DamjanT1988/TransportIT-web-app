﻿// <auto-generated />
using System;
using KnowIT_TransportIT_webapp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KnowIT_TransportIT_webapp.Migrations.FreeDay
{
    [DbContext(typeof(FreeDayContext))]
    [Migration("20230817194252_InitialCreateFreeDay")]
    partial class InitialCreateFreeDay
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.21");

            modelBuilder.Entity("KnowIT_TransportIT_webapp.Models.FreeDayClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndDateFreeDay")
                        .HasColumnType("TEXT");

                    b.Property<string>("FreeDayReason")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartDateFreeDay")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("StatusFreeDay")
                        .IsRequired()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("FreeDayClass");
                });
#pragma warning restore 612, 618
        }
    }
}
