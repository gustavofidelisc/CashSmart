using System.Data.SqlTypes;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contratos;
using CashSmart.Aplicacao.Services.Criptografia.Interface;
using CashSmart.Aplicacao.Services.Jwt;

namespace CashSmart.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IBcryptSenhaService _bcryptSenhaService;
        private readonly IJwtTokenService _jwtTokenService;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio, IBcryptSenhaService bcryptSenhaService, IJwtTokenService jwtTokenService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _bcryptSenhaService =  bcryptSenhaService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Guid> AdicionarUsuarioAsync(Usuario usuario)
        {
            ValidarDadosDoUsuario(usuario);
            await VerificarSeUsuarioExiste(usuario);
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new ArgumentNullException("Senha não pode ser nulo");
            }
            usuario.Senha = _bcryptSenhaService.CriptografarSenha(usuario.Senha);
            usuario.Email = usuario.Email.ToLower();

            return await _usuarioRepositorio.AdicionarUsuarioAsync(usuario);
        }

        public async Task<string> AutenticarUsuarioAsync(string email, string senha)
        {
            var usuario = await _usuarioRepositorio.ObterUsuarioPorEmailAsync(email.ToLower());
            if (usuario == null)
            {
                throw new ArgumentNullException("Usuário não encontrado");
            }
            if (!_bcryptSenhaService.VerificarSenha(senha, usuario.Senha))
            {
                throw new ArgumentNullException("Senha inválida");
            }

            return  _jwtTokenService.GerarTokenJWT(usuario);
            
        }


        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            var usuarioDominio = await _usuarioRepositorio.ObterUsuarioPorIdAsync(usuario.Id, true);
            if (usuarioDominio == null)
            {
                throw new ArgumentNullException("Usuário não encontrado");
            }
            ValidarDadosDoUsuario(usuario);

            usuarioDominio.DataAtualizacao = DateTime.Now;
            usuarioDominio.Nome = usuario.Nome;
            usuarioDominio.Email = usuario.Email.ToLower();

            await _usuarioRepositorio.AtualizarUsuarioAsync(usuarioDominio);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync(bool ativo)
        {
            return await _usuarioRepositorio.ObterUsuariosAsync(ativo);
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(Guid id)
        {
           var usuario = await _usuarioRepositorio.ObterUsuarioPorIdAsync(id, true);
           if (usuario == null)
           {
               throw new SqlNullValueException("Usuário não encontrado");
           }
           return usuario;
        }

        public async Task<Usuario> ObterUsuarioPorEmailAsync(string email)
        {
            var usuario = await _usuarioRepositorio.ObterUsuarioPorEmailAsync(email);
            if (usuario == null)
            {
                throw new SqlNullValueException("Usuário não encontrado");
            }
            return usuario;
        }

        public async Task RemoverUsuarioAsync(Guid id)
        {
            var usuarioDominio =await _usuarioRepositorio.ObterUsuarioPorIdAsync(id, true);
            if (usuarioDominio == null)
            {
                throw new SqlNullValueException("Usuário não encontrado");
            }
            usuarioDominio.Ativo = false;
            await _usuarioRepositorio.AtualizarUsuarioAsync(usuarioDominio);
            
        }

        public async Task RestaurarUsuarioAsync(Guid id)
        {
            var usuarioDominio =await _usuarioRepositorio.ObterUsuarioPorIdAsync(id, false);
            if (usuarioDominio == null)
            {
                throw new SqlNullValueException("Usuário não encontrado");
            }
            usuarioDominio.Ativo = true;
            await _usuarioRepositorio.AtualizarUsuarioAsync(usuarioDominio);
        }

        #region Métodos Privados
        private void ValidarDadosDoUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Usuario não pode ser nulo");
            }


            if (string.IsNullOrEmpty(usuario.Nome))
            {
                throw new ArgumentNullException("Nome não pode ser nulo");
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new ArgumentNullException("Email não pode ser nulo");
            }
     
        }

        private async Task VerificarSeUsuarioExiste(Usuario usuario)
        {
            var usuarioRepositorio = await _usuarioRepositorio.ObterUsuarioPorEmailAsync(usuario.Email);
            if (usuarioRepositorio != null)
            {
                throw new ArgumentException("Email já cadastrado");
            }
        }

        
        #endregion
    }
}