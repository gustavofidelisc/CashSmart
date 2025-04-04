using System.Data.SqlTypes;
using CashSmart.API.Models;
using CashSmart.API.Models.Requisicao;
using CashSmart.API.Models.Resposta;
using CashSmart.API.Models.Exceptions;
using CashSmart.Dominio.Entidades;
using CashSmart.Dominio.Enumeradores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Name;

namespace CashSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {

        public ICategoriaAplicacao _categoriaAplicacao { get; set; }
        public CategoriaController(ICategoriaAplicacao categoriaAplicacao)
        {
            _categoriaAplicacao = categoriaAplicacao;
        }
        [Authorize]
        [HttpPost]
        [Route("Criar")]
        public async Task<IActionResult> AdcionarCategoriaAsync([FromBody] CategoriaCriar categoria)
        {
            try
            {
                var categoriaDominio = new Categoria
                {
                    Nome = categoria.Nome,
                    TipoTransacao = categoria.TipoTransacao,
                    UsuarioId = this.ObterUsuarioIdDoHeader()
                };

                int categoriaId =  await _categoriaAplicacao.AdicionarCategoriaAsync(categoriaDominio);

                return Ok(categoriaId);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarCategorias()
        {
            try
            {
                Guid usuarioId = this.ObterUsuarioIdDoHeader();
                var categorias = await _categoriaAplicacao.ObterTodasCategoriasUsuarioAsync(usuarioId);

                var categoriasResposta = categorias.Select(c => new CategoriaResposta
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    TipoTransacao = Enum.GetName(typeof(TipoDaTransacao), c.TipoTransacao)
                }).ToList();
                return Ok(categoriasResposta);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("ObterPorId")]
        public async Task<IActionResult> ObterCategoriaPorId([FromQuery] int id)
        {
            try
            {
                var categoria = await _categoriaAplicacao.ObterCategoriaPorIdAsync(id, this.ObterUsuarioIdDoHeader());

                var categoriaReposta = new CategoriaResposta{
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    TipoTransacao = Enum.GetName(typeof(TipoDaTransacao), categoria.TipoTransacao)
                };
                return Ok(categoriaReposta);
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("Atualizar")]     
        public async Task<IActionResult> AtualizarCategoria([FromBody] CategoriaAtualizar categoria)
        {
            try
            {
                var categoriaDominio = new Categoria
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    TipoTransacao = categoria.TipoTransacao,
                    UsuarioId = this.ObterUsuarioIdDoHeader()
                };

                await _categoriaAplicacao.AtualizarCategoriaAsync(categoriaDominio, this.ObterUsuarioIdDoHeader());
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Deletar/{id}")]
        public async Task<IActionResult> DeletarCategoria([FromRoute] int id)
        {
            try
            {
                await _categoriaAplicacao.RemoverCategoriaAsync(id, this.ObterUsuarioIdDoHeader());
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }
    }
}