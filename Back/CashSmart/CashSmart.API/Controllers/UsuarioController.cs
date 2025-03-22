
using Microsoft.AspNetCore.Mvc;
using CashSmart.Dominio.Entidades;
using CashSmart.Aplicacao.Interface;
using CashSmart.API.Models.Usuario.Resposta;

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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("Listar")]
    public async Task<IActionResult> ListarUsuariosAsync()
    {
        try
        {
            var usuarios = await _usuarioAplicacao.ObterTodosUsuariosAsync();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("ObterPorId/{id}")]
    public async Task<IActionResult> ObterUsuarioPorIdAsync([FromRoute]int id)
    {
        try
        {
            var usuario = await _usuarioAplicacao.ObterUsuarioPorIdAsync(id);
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

    [HttpPut]
    [Route("Atualizar/{id}")]
    public async Task<IActionResult> AtualizarUsuarioAsync([FromRoute]int id,[FromBody] Usuario usuario)
    {
        try
        {
            usuario.Id = id;
            await _usuarioAplicacao.AtualizarUsuarioAsync(usuario);
            return Ok(usuario);
        }
        catch(ArgumentNullException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Route("Deletar/{id}")]
    public async Task<IActionResult> DeletarUsuarioAsync([FromRoute]int id)
    {
        try
        {
            await _usuarioAplicacao.RemoverUsuarioAsync(id);
            return Ok();
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

}