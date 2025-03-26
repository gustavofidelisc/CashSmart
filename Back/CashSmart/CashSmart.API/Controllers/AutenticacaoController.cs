using System.Data.SqlTypes;
using CashSmart.API.Models.Usuario.Requisicao;
using CashSmart.Aplicacao.Interface;
using CashSmart.Servicos.Services.Criptografia.Interface;
using CashSmart.Servicos.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IBcryptSenhaService _bcryptSenhaService;
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        public AutenticacaoController(IJwtTokenService jwtTokenService, IBcryptSenhaService bcryptSenhaService, IUsuarioAplicacao usuarioAplicacao)
        {
            _jwtTokenService = jwtTokenService;
            _bcryptSenhaService = bcryptSenhaService;
            _usuarioAplicacao = usuarioAplicacao;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            try
            {
                var usuario = await _usuarioAplicacao.ObterUsuarioPorEmailAsync(usuarioLogin.Email);
                if(usuario == null)
                {
                    return StatusCode(400, "Usuário ou senha inválidos");
                }

                if(!_bcryptSenhaService.VerificarSenha(usuarioLogin.Senha, usuario.Senha))
                {
                    return StatusCode(400, "Usuário ou senha inválidos");
                }
                return Ok(_jwtTokenService.GerarTokenJWT(usuario));
            }
            catch (SqlNullValueException)
            {
                return StatusCode(400, "Usuário ou senha inválidos");
            }
        }
    }
}