﻿// <auto-generated />
using HotelAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HotelAPI.Data.Migrations
{
    [DbContext(typeof(HotelContext))]
    partial class HotelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HotelAPI.Domain.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int?>("RegionId");

                    b.Property<int>("Rooms");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("HotelAPI.Domain.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("HotelAPI.Domain.Hotel", b =>
                {
                    b.HasOne("HotelAPI.Domain.Region")
                        .WithMany("Hotels")
                        .HasForeignKey("RegionId");
                });
#pragma warning restore 612, 618
        }
    }
}
