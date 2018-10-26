﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SecretHitler.API.Repositories;
using SecretHitler.Models.Entities;
using System;

namespace SecretHitler.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20171214120007_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SecretHitler.Models.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateCreated")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("GameStateId");

                    b.Property<string>("JoinKey");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("SecretHitler.Models.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("SecretHitler.Models.Entities.Player", b =>
                {
                    b.HasOne("SecretHitler.Models.Entities.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}