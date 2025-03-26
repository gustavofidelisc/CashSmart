using System.Data.SqlTypes;
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
                    TipoTransacao = categoria.TipoTransacao
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
    }
}