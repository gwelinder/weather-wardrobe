﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeatherWardrobeApi.Data;

#nullable disable

namespace WeatherWardrobeApi.Migrations
{
    [DbContext(typeof(WeatherWardrobeContext))]
    [Migration("20241004164014_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WeatherWardrobeApi.Models.ClothingItem", b =>
                {
                    b.Property<int>("ClothingItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ClothingItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WeatherConditionId")
                        .HasColumnType("integer");

                    b.HasKey("ClothingItemId");

                    b.HasIndex("WeatherConditionId");

                    b.ToTable("ClothingItems");
                });

            modelBuilder.Entity("WeatherWardrobeApi.Models.WeatherCondition", b =>
                {
                    b.Property<int>("WeatherConditionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WeatherConditionId"));

                    b.Property<string>("ConditionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TemperatureRange")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WeatherConditionId");

                    b.ToTable("WeatherConditions");
                });

            modelBuilder.Entity("WeatherWardrobeApi.Models.ClothingItem", b =>
                {
                    b.HasOne("WeatherWardrobeApi.Models.WeatherCondition", "WeatherCondition")
                        .WithMany("ClothingItems")
                        .HasForeignKey("WeatherConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherCondition");
                });

            modelBuilder.Entity("WeatherWardrobeApi.Models.WeatherCondition", b =>
                {
                    b.Navigation("ClothingItems");
                });
#pragma warning restore 612, 618
        }
    }
}
