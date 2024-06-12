﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspcorebackend.Config;

#nullable disable

namespace aspcorebackend.Migrations
{
    [DbContext(typeof(BookNavDbContext))]
    [Migration("20240607085438_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("aspcorebackend.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR");

                    b.HasKey("AuthorId");

                    b.ToTable("authors", (string)null);
                });

            modelBuilder.Entity("aspcorebackend.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("BookId"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("PublishYear")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("aspcorebackend.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR");

                    b.HasKey("CategoryId");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("aspcorebackend.Models.Book", b =>
                {
                    b.HasOne("aspcorebackend.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("aspcorebackend.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Author");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("aspcorebackend.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("aspcorebackend.Models.Category", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}