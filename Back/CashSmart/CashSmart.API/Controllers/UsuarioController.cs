
using Microsoft.AspNetCore.Mvc;
using CashSmart.Dominio.Entidades;
using CashSmart.Aplicacao.Interface;
using CashSmart.API.Models.Usuario.Resposta;
using System.Data.SqlTypes;
using Microsoft.IdentityModel.Tokens;
using CashSmart.API.Models.Usuario.Requisicao;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]

public class  UsuarioController : ControllerBase
{
    private readonly IUsuarioAplicacao _usuarioAplicacao;

    public UsuarioController(IUsuarioAplicacao usuarioAplicacao)
    {
        _usuarioAplicacao = usuarioAplicacao;
    }


    [HttpPost]
    [Route("Criar")]
    public async Task<IActionResult> AdicionarUsuarioAsync([FromBody]UsuarioCriar usuario)
    {
        try
        {

            if(usuario.Senha != usuario.ConfirmacaoSenha)
            {
                return BadRequest("As senhas não conferem");
            }
            var usuarioDominio = new Usuario
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            };
            await _usuarioAplicacao.AdicionarUsuarioAsync(usuarioDominio);
            return Ok(usuario);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    [Route("Listar")]
    public async Task<IActionResult> ListarUsuariosAsync([FromQuery]bool ativo = true)
    {
        try
        {
            var usuarios = await _usuarioAplicacao.ObterTodosUsuariosAsync(ativo);
            IEnumerable<UsuarioResposta> usuariosResposta = usuarios.Select(usuario => new UsuarioResposta
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
            });
            return Ok(usuariosResposta);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ObterUsuarioPorIdAsync()
    {
        try
        {
            Guid usuarioId= this.ObterUsuarioIdDoHeader();

            var usuario = await _usuarioAplicacao.ObterUsuarioPorIdAsync(usuarioId);

            UsuarioResposta usuarioResposta = new UsuarioResposta
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
            return Ok(usuarioResposta);
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

    [HttpPut]
    [Authorize]
    [Route("Atualizar")]
    public async Task<IActionResult> AtualizarUsuarioAsync([FromBody] UsuarioAtualizar usuario)
    {
        try
        {

            Guid usuarioId= this.ObterUsuarioIdDoHeader();
            var UsuarioAtualizar = new Usuario
            {
                Id = usuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email
            };
            await _usuarioAplicacao.AtualizarUsuarioAsync(UsuarioAtualizar);
            return Ok(usuario);
            }
        catch(SqlNullValueException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize]
    [Route("Deletar")]
    public async Task<IActionResult> DeletarUsuarioAsync()
    {
        try
        {
            Guid usuarioId= this.ObterUsuarioIdDoHeader();
            await _usuarioAplicacao.RemoverUsuarioAsync(usuarioId);
            return Ok();
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

    [HttpPost]
    [Authorize]
    [Route("Restaurar/{id}")]
    public async Task<IActionResult> RestaurarUsuarioAsync([FromRoute]Guid id)
    {
        try
        {
            await _usuarioAplicacao.RestaurarUsuarioAsync(id);
            return Ok();
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
}