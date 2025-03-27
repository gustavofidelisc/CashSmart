using System.Data.SqlTypes;
using CashSmart.API.Models.Autenticacao.Resposta;
using CashSmart.API.Models.Exceptions;
using CashSmart.API.Models.Usuario.Requisicao;
using CashSmart.Aplicacao.Interface;
using CashSmart.Servicos.Services.Criptografia.Interface;
using CashSmart.Servicos.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
                    throw new ArgumentException("Usuário ou senha inválidos");
                }

                if(!_bcryptSenhaService.VerificarSenha(usuarioLogin.Senha, usuario.Senha))
                {
                    throw new ArgumentException("Usuário ou senha inválidos");
                }

                var resposta = new Autenticacao
                {
                    Token = _jwtTokenService.GerarTokenJWT(usuario)
                };
                return Ok(new Autenticacao{
                    Token = resposta.Token
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