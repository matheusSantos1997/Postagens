using Dapper;
using Postagens.Domain.Models;
using Postagens.Repository.Data;
using Postagens.Repository.Interfaces;
using Postagens.Repository.Repositories.DbQueries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Repository.Repositories
{
    public class ImagemRepository : IImagemRepository
    {
        private readonly IDbConnection _connection;

        public ImagemRepository()
        {
            _connection = DataAccess.GetConnection();
        }

        public async Task<List<Imagem>> MostrarTodasImagensPost()
        {
            try
            {
                List<Imagem> imagens = new();

                string query = ImagemDbScript.SelectAllImagens();

                var result = await _connection.QueryAsync<Imagem, Post, Imagem>(query,
                    map: (imagem, post) =>
                    {
                        if(imagens.FirstOrDefault(i => i.Id == imagem.Id)  == null)
                        {
                            imagem.Post = post;
                            imagens.Add(imagem);
                        }
                        else
                        {
                            imagem = imagens.FirstOrDefault(i => post.Id == imagem.Id);
                        }

                        return imagem;
                    });

                result = imagens;

                return result.ToList();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Imagem> MostrarImagemPorId(long id)
        {
            try
            {
                List<Imagem> imagens = new();

                string query = ImagemDbScript.SelectImagemById();

                var result = await _connection.QueryAsync<Imagem, Post, Imagem>(query,
                    map: (imagem, post) =>
                    {
                        if (imagens.FirstOrDefault(i => i.Id == imagem.Id) == null)
                        {
                            imagem.Post = post;
                            imagens.Add(imagem);
                        }
                        else
                        {
                            imagem = imagens.FirstOrDefault(i => post.Id == imagem.Id);
                        }

                        return imagem;
                    }, new { Id = id });

                return result.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
