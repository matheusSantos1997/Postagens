using Postagens.Domain.Dtos;
using Postagens.Domain.Models;
using Postagens.Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Application.Interfaces
{
    public interface IPostService
    {
        // Task<List<Post>> ListarTodosPosts();
        Task<PageList<Post>> ListarTodosPosts(PageParams pageParams);

        Task<Post> ListarPostPorId(long id);

        // Task<List<Post>> ListarTodosPorTitulo(string titulo);
        Task<PageList<Post>> ListarTodosPorTitulo(string titulo, PageParams pageParams);

        Task<Post> CadastrarNovoPost(PostCreateDTO postDto);

        Task<Post> AtualizarPost(long id, PostUpdateDTO postDto);

        Task<bool> DeletarPost(long id);
    }
}
