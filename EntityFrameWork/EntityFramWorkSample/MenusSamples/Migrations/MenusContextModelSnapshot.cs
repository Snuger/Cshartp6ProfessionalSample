﻿// <auto-generated />
using System;
using MenusSamples;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MenusSamples.Migrations
{
    [DbContext(typeof(MenusContext))]
    partial class MenusContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("MenusSamples.Menu", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MenuCardId");

                    b.Property<decimal?>("Price");

                    b.Property<string>("Text");

                    b.HasKey("MenuId");

                    b.HasIndex("MenuCardId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("MenusSamples.MenuCard", b =>
                {
                    b.Property<int>("MenuCardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("MenuCardId");

                    b.ToTable("MenuCards");
                });

            modelBuilder.Entity("MenusSamples.Menu", b =>
                {
                    b.HasOne("MenusSamples.MenuCard", "MenuCard")
                        .WithMany("Menus")
                        .HasForeignKey("MenuCardId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
