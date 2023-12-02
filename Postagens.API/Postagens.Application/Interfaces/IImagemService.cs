using Postagens.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Application.Interfaces
{
    public interface IImagemService
    {
        Task<Imagem> SalvarImagem(Imagem imagem);

        Task<Imagem> AtualizarImagem(long id, Imagem imagem);

        Task<bool> DeletarImagem(long id);

        Task<List<Imagem>> ListarTodasImagens();

        Task<Imagem> ListarImagemPorId(long id);
    }
}
