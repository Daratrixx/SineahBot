﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SineahBot.DataContext;

namespace SineahBot.Database.Migrations
{
    [DbContext(typeof(SineahDbContext))]
    [Migration("20220213113720_FixConfiguration")]
    partial class FixConfiguration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CharacterAncestry")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("Human");

                    b.Property<string>("CharacterClass")
                        .HasColumnType("TEXT");

                    b.Property<int>("Experience")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gold")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pronouns")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterEquipmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdCharacter")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdCharacter");

                    b.ToTable("CharacterEquipment");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterItemEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdCharacter")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

                    b.Property<int>("StackSize")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IdCharacter");

                    b.ToTable("CharacterItems");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterMessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdCharacter")
                        .HasColumnType("TEXT");

                    b.Property<string>("IdRoom")
                        .HasColumnType("TEXT");

                    b.Property<string>("Message")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdCharacter");

                    b.ToTable("CharacterMessages");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.PlayerEntity", b =>
                {
                    b.Property<ulong>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("IdCharacter")
                        .HasColumnType("TEXT");

                    b.Property<string>("Settings")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterEquipmentEntity", b =>
                {
                    b.HasOne("SineahBot.Database.Entities.CharacterEntity", "Character")
                        .WithMany("Equipments")
                        .HasForeignKey("IdCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterItemEntity", b =>
                {
                    b.HasOne("SineahBot.Database.Entities.CharacterEntity", "Character")
                        .WithMany("Items")
                        .HasForeignKey("IdCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterMessageEntity", b =>
                {
                    b.HasOne("SineahBot.Database.Entities.CharacterEntity", "Character")
                        .WithMany("Messages")
                        .HasForeignKey("IdCharacter")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("SineahBot.Database.Entities.CharacterEntity", b =>
                {
                    b.Navigation("Equipments");

                    b.Navigation("Items");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
