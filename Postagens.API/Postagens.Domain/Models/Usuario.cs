using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Domain.Models
{
    public class Usuario
    {
        [Key]
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }

        public string CpfCnpj { get; set; }

        public string Cep { get; set; }

        public string Rua { get; set; }

        public int Numero { get; set; }

        #nullable enable
        public string? Complemento { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string Email { get; set; }

        public string NomeUsuario { get; set; }

        public string Senha { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }
}
