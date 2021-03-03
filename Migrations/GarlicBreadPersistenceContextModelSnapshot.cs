﻿// <auto-generated />

using System;
using GarlicBread.Persistence.Document;
using GarlicBread.Persistence.Relational;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GarlicBread.Migrations
{
    [DbContext(typeof(GarlicBreadPersistenceContext))]
    internal class GarlicBreadPersistenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity(
                "GarlicBread.Persistence.Relational.JsonRow<GarlicBread.Persistence.Document.GuildConfig>", b =>
                {
                    b.Property<decimal>("GuildId")
                        .HasColumnType("numeric(20,0)");

                    b.Property<GuildConfig>("Data")
                        .HasColumnType("jsonb");

                    b.HasKey("GuildId");

                    b.ToTable("GuildConfigurations");
                });

            modelBuilder.Entity("GarlicBread.Persistence.Relational.Reminder", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("integer")
                    .HasAnnotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                b.Property<decimal>("ChannelId")
                    .HasColumnType("numeric(20,0)");

                b.Property<DateTimeOffset>("CreatedAt")
                    .HasColumnType("timestamp with time zone");

                b.Property<decimal>("CreatorId")
                    .HasColumnType("numeric(20,0)");

                b.Property<DateTimeOffset>("DueAt")
                    .HasColumnType("timestamp with time zone");

                b.Property<decimal>("GuildId")
                    .HasColumnType("numeric(20,0)");

                b.Property<decimal>("MessageId")
                    .HasColumnType("numeric(20,0)");

                b.Property<string>("Text")
                    .HasMaxLength(100)
                    .HasColumnType("character varying(100)");

                b.HasKey("Id");

                b.ToTable("Reminders");
            });
#pragma warning restore 612, 618
        }
    }
}