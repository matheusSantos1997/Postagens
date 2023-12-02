using Postagens.Domain.Models;
using Postagens.Repository.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Repository.Interfaces
{
    public interface IPostRepository
    {
        //Task<List<Post>> PegarTodosPosts();
        Task<PageList<Post>> PegarTodosPosts(PageParams pageParams);

        // Task<List<Post>> FiltrarPostsPorTitulo(string titulo);
        Task<PageList<Post>> FiltrarPostsPorTitulo(string titulo, PageParams pageParams);

        Task<Post> ListarPostPorId(long id);
    }
}
