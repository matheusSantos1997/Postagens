﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postagens.Repository.Data;

#nullable disable

namespace Postagens.Repository.Migrations
{
    [DbContext(typeof(DataAccess))]
    partial class DataAccessModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Postagens.Domain.Models.Imagem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("SalvoEm")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("URLImagem")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("Imagens", (string)null);
                });

            modelBuilder.Entity("Postagens.Domain.Models.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<long?>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Posts", (string)null);
                });

            modelBuilder.Entity("Postagens.Domain.Models.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Postagens.Domain.Models.Imagem", b =>
                {
                    b.HasOne("Postagens.Domain.Models.Post", "Post")
                        .WithOne("Imagem")
                        .HasForeignKey("Postagens.Domain.Models.Imagem", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Postagens.Domain.Models.Post", b =>
                {
                    b.HasOne("Postagens.Domain.Models.Usuario", "Usuario")
                        .WithMany("Posts")
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Postagens.Domain.Models.Post", b =>
                {
                    b.Navigation("Imagem");
                });

            modelBuilder.Entity("Postagens.Domain.Models.Usuario", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
