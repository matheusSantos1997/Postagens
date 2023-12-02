using Microsoft.AspNetCore.Mvc;
using Postagens.API.Extensions;
using Postagens.Application.Interfaces;
using Postagens.Domain.Dtos;
using Postagens.Repository.Pagination;

namespace Postagens.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;    

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [Route("PegarTodosPosts")]
        public async Task<IActionResult> PegarTodosPosts([FromQuery]PageParams pageParams)
        {
            var posts = await _postService.ListarTodosPosts(pageParams);

            if (!posts.Any()) return NoContent();

            Response.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);

            return Ok(posts);
        }

        [HttpGet]
        [Route("FiltrarTodosPostsPorTitulo/{titulo}")]
        public async Task<IActionResult> FiltrarTodosPostsPorTitulo(string titulo, [FromQuery]PageParams pageParams)
        {
            var posts = await _postService.ListarTodosPorTitulo(titulo, pageParams);

            if(!posts.Any()) return NoContent();

            Response.AddPagination(posts.CurrentPage, posts.PageSize, posts.TotalCount, posts.TotalPages);

            return Ok(posts);
        }

        [HttpGet]
        [Route("ListarPostPorId/{id}")]
        public async Task<IActionResult> ListarPostPorId(long id)
        {
            var post = await _postService.ListarPostPorId(id);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        [Route("CadastrarNovoPost")]
        public async Task<IActionResult> CadastrarNovoPost([FromBody]PostCreateDTO model)
        {
            var post = await _postService.CadastrarNovoPost(model);

            if(post == null) return BadRequest();

            return Created($"/api/posts/CadastrarNovoPost/{model.Id}", post);
        }

        [HttpPut]
        [Route("AtualizarPost/{id}")]
        public async Task<IActionResult> AtualizarPost(long id, [FromBody]PostUpdateDTO model)
        {
            var post = await _postService.AtualizarPost(id, model);

            if(post == null) return BadRequest(); 
            
            return Ok(post);
        }

        [HttpDelete]
        [Route("ExcluirPost/{id}")]
        public async Task<IActionResult> ExcluirPost(long id)
        {

            try
            {
                var deleted = await _postService.DeletarPost(id);

                if (deleted == false) return BadRequest();

                return Ok(new { message = $"Post deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
           
        }
    }
}
