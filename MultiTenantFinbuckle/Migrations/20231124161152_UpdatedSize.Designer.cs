﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MulltiTenantFinbuckle.Db;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MulltiTenantFinbuckle.Migrations
{
    [DbContext(typeof(TenantAdminDbContext))]
    [Migration("20231124161152_UpdatedSize")]
    partial class UpdatedSize
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MulltiTenantFinbuckle.Models.MultiTenantInfo", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("AuthServerUrl")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("ConnectionString")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Identifier")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Name")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Realm")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Resource")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<string>("Secret")
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("tenant");
                });
#pragma warning restore 612, 618
        }
    }
}