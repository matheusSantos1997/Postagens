using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Postagens.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Repository.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.Id).HasName("id");

            builder.Property(u => u.Nome).IsRequired().HasColumnName("nome").HasColumnType("varchar(50)");

            builder.Property(u => u.Telefone).IsRequired().HasColumnName("telefone").HasColumnType("varchar(12)");

            builder.Property(u => u.CpfCnpj).IsRequired().HasColumnName("cpfCnpj").HasColumnType("varchar(32)");

            builder.Property(u => u.Cep).IsRequired().HasColumnName("cep").HasColumnType("varchar(9)");

            builder.Property(u => u.Rua).IsRequired().HasColumnName("rua").HasColumnType("varchar(100)");

            builder.Property(u => u.Numero).IsRequired().HasColumnName("numero").HasColumnType("int");

            builder.Property(u => u.Complemento).HasColumnName("complemento").HasColumnType("varchar(50)");

            builder.Property(u => u.Bairro).IsRequired().HasColumnName("bairro").HasColumnType("varchar(100)");

            builder.Property(u => u.Localidade).IsRequired().HasColumnName("localidade").HasColumnType("varchar(100)");

            builder.Property(u => u.Cidade).IsRequired().HasColumnName("cidade").HasColumnType("varchar(100)");

            builder.Property(u => u.Uf).IsRequired().HasColumnName("uf").HasColumnType("varchar(5)");

            builder.Property(u => u.Email).IsRequired().HasColumnName("email").HasColumnType("varchar(100)");

            builder.Property(u => u.NomeUsuario).IsRequired().HasColumnName("nomeUsuario").HasColumnType("varchar(50)");

            builder.Property(u => u.Senha).IsRequired().HasColumnName("senha").HasColumnType("varchar(255)");

            builder.HasMany(u => u.Posts)
                   .WithOne(p => p.Usuario);

            builder.ToTable("Usuarios");
        }
    }
}
