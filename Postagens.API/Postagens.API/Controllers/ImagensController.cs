using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Postagens.Application.Interfaces;
using Postagens.Domain.Models;
using System.Net.Http.Headers;

namespace Postagens.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagensController : ControllerBase
    {
        private readonly IImagemService _imageService;

        private readonly IWebHostEnvironment _hostEnvironment;


        public ImagensController(IImagemService imagemService, IWebHostEnvironment hostEnvironment)
        {
            _imageService = imagemService;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        [Route("GetAllImagens")]
        public async Task<IActionResult> GetAllImagens()
        {
            try
            {
                var imagens = await _imageService.ListarTodasImagens();

                if (!imagens.Any()) return NoContent();

                return Ok(imagens);
            }
            catch (IOException ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetImagensById/{id}")]
        public async Task<IActionResult> GetImagensById(long id)
        {
            try
            {
                var imagem = await _imageService.ListarImagemPorId(id);

                if(imagem == null) return NoContent();

                return Ok(imagem);
            }
            catch(IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("UploadImagem/{postId}")]
        public async Task<IActionResult> UploadImagem(long postId)
        {
            try
            {
                bool extensaoValida = false;

                string[] extensions = { ".jpeg", ".jpg", ".png" };

                var file = Request.Form.Files[0];

                var folderName = Path.Combine("Images"); // pega o diretorio onde vai salvar

                string extension = Path.GetExtension(file.FileName);

                // combina o diretorio onde vai armazerar + diretorio da aplicaçao
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName); // salva no diretorio

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                if(file.Length > 0)
                {
                    // vai pegar o nome do arquivo e montar o arquivo
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                    fileName = Guid.NewGuid().ToString() + extension;

                    // se vier aspas duplas ou espaçamentos no nome do arquivo, vai ser removido
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                    foreach (string ex in extensions)
                    {
                        if (extension == ex)
                        {
                            extensaoValida = true;
                        }
                    }

                    if(extensaoValida)
                    {
                        Imagem img = new Imagem();
                        img.Nome = fileName;
                        img.URLImagem = fullPath;
                        img.SalvoEm = DateTime.Now;
                        img.PostId = postId;

                        // salva o arquivo
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream); // realiza uma copia para o stream
                        }

                        await _imageService.SalvarImagem(img);
                    }
                    else
                    {
                        if (extension != extensions.Length.ToString())
                        {
                            return BadRequest(new
                            {
                                menssagem = $"only jpeg, jpg or png."
                            });
                        }
                    }
                }

                return Ok(new
                {
                    message = $"upload was successful."
                });
            }
            catch(IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateImagem/{id}")]
        public async Task<IActionResult> UpdateImagem(long id)
        {
            try
            {
                bool extensaoValida = false;

                string[] extensions = { ".jpeg", ".jpg", ".png" };

                var file = Request.Form.Files[0];

                if (file is null)
                {
                    throw new ArgumentNullException(nameof(file));
                }

                var folderName = Path.Combine("Images"); // pega o diretorio onde vai salvar

                string extension = Path.GetExtension(file.FileName);

                // combina o diretorio onde vai armazerar + diretorio da aplicaçao
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName); // salva no diretorio

                if(file.Length > 0)
                {
                    // vai pegar o nome do arquivo e montar o arquivo
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;

                    fileName = Guid.NewGuid().ToString() + extension;

                    // se vier aspas duplas ou espaçamentos no nome do arquivo, vai ser removido
                    var fullPath = Path.Combine(pathToSave, fileName.Replace("\"", " ").Trim());

                    foreach (string ex in extensions)
                    {
                        if (extension == ex)
                        {
                            extensaoValida = true;
                        }
                    }

                    if(extensaoValida)
                    {
                        var imagem = await _imageService.ListarImagemPorId(id);

                        if (imagem == null) return NotFound();

                        Imagem img = new Imagem();
                        img.Nome = fileName;
                        img.URLImagem = fullPath;
                        img.SalvoEm = DateTime.Now;
                        img.PostId = imagem.Post.Id;

                        // atualiza o caminho da imagem
                        Directory.Move(imagem.URLImagem, img.URLImagem);

                        // salva o arquivo
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        await _imageService.AtualizarImagem(id, img);

                        return Ok(new
                        {
                            message = $"image was updated successful."
                        });

                    }
                    else
                    {
                        return BadRequest(new
                        {
                            message = $"only jpeg, jpg or png."
                        });
                    }
                }

                return BadRequest();
            }
            catch (IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }  
        }

        [HttpDelete]
        [Route("DeleteImagem/{id}")]
        public async Task<IActionResult> DeleteImagem(long id)
        {
            try
            {
                var imagem = await _imageService.ListarImagemPorId(id);

                if (imagem == null)
                {
                    return NotFound();
                }

                string imageName = imagem.URLImagem;

                // Obter o caminho do arquivo antes de excluir do banco de dados
                var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Images", imageName);

                // Excluir o post do banco de dados
                await _imageService.DeletarImagem(imagem.Id);

                // Verificar se o arquivo existe antes de tentar excluí-lo
                if (System.IO.File.Exists(imagePath))
                {
                    // Tentar excluir o arquivo
                    try
                    {
                        System.IO.File.Delete(imagePath);
                        Console.WriteLine($"Arquivo excluído com sucesso: {imagePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao excluir o arquivo: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"O arquivo não existe no caminho: {imagePath}");
                }

                return Ok(new
                {
                    message = "Imagem excluída com sucesso"
                });
            }
            catch (IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
