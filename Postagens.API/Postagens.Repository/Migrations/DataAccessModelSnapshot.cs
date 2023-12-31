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

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("bairro");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(9)")
                        .HasColumnName("cep");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("cidade");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("complemento");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasColumnType("varchar(32)")
                        .HasColumnName("cpfCnpj");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("localidade");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nome");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nomeUsuario");

                    b.Property<int>("Numero")
                        .HasColumnType("int")
                        .HasColumnName("numero");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("rua");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("senha");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(12)")
                        .HasColumnName("telefone");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("varchar(5)")
                        .HasColumnName("uf");

                    b.HasKey("Id")
                        .HasName("id");

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
                    b.Navigation("Imagem")
                        .IsRequired();
                });

            modelBuilder.Entity("Postagens.Domain.Models.Usuario", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
