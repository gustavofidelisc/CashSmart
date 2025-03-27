using System.Data.SqlTypes;
using System.Security.Claims;
using CashSmart.API.Models.Categoria;
using CashSmart.Dominio.Entidades;
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
                    UsuarioId = categoria.UsuarioId
                };

                int categoriaId =  await _categoriaAplicacao.AdicionarCategoriaAsync(categoriaDominio);

                return Ok(categoriaId);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> ListarCategorias()
        {
            try
            {
                var categorias = await _categoriaAplicacao.ObterTodasCategoriasAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObterPorId")]
        public async Task<IActionResult> ObterCategoriaPorId([FromQuery] int id)
        {
            try
            {
                var categoria = await _categoriaAplicacao.ObterCategoriaPorIdAsync(id);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Métodos Privados
        private Guid ObterIdUsuarioHeader(){
            var claimValue = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(claimValue, out Guid userId))
            {
                throw new ArgumentException("Id do usuário inválido");
            }
            return userId;
        }
        #endregion
    }
}