
using Microsoft.AspNetCore.Mvc;
using CashSmart.Dominio.Entidades;
using CashSmart.Aplicacao.Interface;

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
    public async Task<IActionResult> AdicionarUsuarioAsync(Usuario usuario)
    {
        try
        {
            await _usuarioAplicacao.AdicionarUsuarioAsync(usuario);
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // [HttpGet]
    // [Route("Listar")]
    // public  Task<IActionResult> ListarUsuariosAsync()
    // {
    //     try
    //     {
    //         //var usuarios = await _usuarioAplicacao.ListarUsuariosAsync();
    //         //return Ok(usuarios);
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    // }
}