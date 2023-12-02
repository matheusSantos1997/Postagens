using Postagens.Application.Interfaces;
using Postagens.Domain.Dtos;
using Postagens.Domain.Models;
using Postagens.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postagens.Repository.Pagination;

namespace Postagens.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository _genericRepository;

        private readonly IPostRepository _postRepository;

        public PostService(IGenericRepository genericRepository, IPostRepository postRepository)
        {
            _genericRepository = genericRepository;
            _postRepository = postRepository;
        }

        public async Task<Post> CadastrarNovoPost(PostCreateDTO postDto)
        {
            try
            {

                // var usuarioId = User.Identity.IsAuthenticated;

                Post post = new()
                {
                    Titulo = postDto.Titulo,
                    Conteudo = postDto.Conteudo,
                };

                _genericRepository.Adicionar(post);

                bool save = await _genericRepository.SaveChangesAsync();

                if (save)
                {
                    return await _postRepository.ListarPostPorId(post.Id);
                }

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Post> AtualizarPost(long id, PostUpdateDTO postDto)
        {
            try
            {
                var p = await _postRepository.ListarPostPorId(id);

                Post post = new()
                {
                    Id = postDto.Id,
                    Titulo = postDto.Titulo,
                    Conteudo = postDto.Conteudo,
                };

                post.Id = p.Id;

                if (p == null) return null;

                _genericRepository.Atualizar(post);

                bool save = await _genericRepository.SaveChangesAsync();

                if (save)
                {
                    return await _postRepository.ListarPostPorId(post.Id);
                }

                return null;
            }
            catch(DbUpdateException ex)
            {
                throw new DbUpdateException(ex.Message);
            }
        }

        public async Task<bool> DeletarPost(long id)
        {
            try
            {
                var post = await _postRepository.ListarPostPorId(id);

                if(post == null) return false;

                _genericRepository.Excluir(post);

                return await _genericRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Post>> ListarTodosPosts(PageParams pageParams)
        {
            try
            {
                var posts = await _postRepository.PegarTodosPosts(pageParams);

                if (posts == null) return null;

                return posts;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageList<Post>> ListarTodosPorTitulo(string titulo, PageParams pageParams)
        {
            try
            {
                var posts = await _postRepository.FiltrarPostsPorTitulo(titulo, pageParams);

                if (posts == null) return null;

                return posts;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Post> ListarPostPorId(long id)
        {
            try
            {
                var post = await _postRepository.ListarPostPorId(id);

                if(post == null) return null;

                return post;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
