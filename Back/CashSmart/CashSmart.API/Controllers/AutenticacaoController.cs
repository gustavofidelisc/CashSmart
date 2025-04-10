using System.Data.SqlTypes;
using CashSmart.API.Models.Autenticacao.Resposta;
using CashSmart.API.Models.Exceptions;
using CashSmart.API.Models.Usuario.Requisicao;
using CashSmart.Aplicacao.Interface;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CashSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        public AutenticacaoController( IUsuarioAplicacao usuarioAplicacao)
        {
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            try
            {
                var token = await _usuarioAplicacao.AutenticarUsuarioAsync(usuarioLogin.Email, usuarioLogin.Senha);
                return Ok(new Autenticacao{
                    Token = token
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