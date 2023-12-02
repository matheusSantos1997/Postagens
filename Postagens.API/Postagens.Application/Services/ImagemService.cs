using Postagens.Application.Interfaces;
using Postagens.Domain.Models;
using Postagens.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postagens.Application.Services
{
    public class ImagemService : IImagemService
    {
        private readonly IGenericRepository _genericRepository;

        private readonly IImagemRepository _imagemRepository;

        public ImagemService(IGenericRepository genericRepository, IImagemRepository imagemRepository)
        {
            _genericRepository = genericRepository;
            _imagemRepository = imagemRepository;
        }

        public async Task<Imagem> SalvarImagem(Imagem imagem)
        {
            try
            {
                _genericRepository.Adicionar(imagem);

                bool save = await _genericRepository.SaveChangesAsync();

                if(save)
                {
                    return imagem;
                }

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Imagem> AtualizarImagem(long id, Imagem imagem)
        {
            try
            {
                var img = await _imagemRepository.MostrarImagemPorId(id);

                imagem.Id = img.Id;

                if (img == null) return null;

                _genericRepository.Atualizar(imagem);

                bool save = await _genericRepository.SaveChangesAsync();

                if (save)
                {
                    return imagem;
                }

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeletarImagem(long id)
        {
            try
            {
                var imagem = await _imagemRepository.MostrarImagemPorId(id);
                
                if(imagem == null) return false;

                _genericRepository.Excluir(imagem);

                return await _genericRepository.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Imagem>> ListarTodasImagens()
        {
            try
            {
                var imagens = await _imagemRepository.MostrarTodasImagensPost();

                if (imagens == null) return null;

                return imagens;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Imagem> ListarImagemPorId(long id)
        {
            try
            {
                var imagem = await _imagemRepository.MostrarImagemPorId(id);

                if (imagem == null) return null;

                return imagem;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
