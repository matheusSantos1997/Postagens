using Postagens.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Domain.Dtos
{
    public class PostUpdateDTO
    {
        [Key]
        public long Id { get; set; }

        public string Titulo { get; set; }

        public string Conteudo { get; set; }


        public Imagem? Imagem { get; set; }

        public PostUpdateDTO(long id, string titulo, string conteudo)
        {
            Id = id;
            Titulo = titulo;
            Conteudo = conteudo;
        }
    }
}
