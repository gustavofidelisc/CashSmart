using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contratos;
using CashSmart.Servicos.Services.Criptografia.Interface;

namespace CashSmart.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IBcryptSenhaService _bcryptSenhaService;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio, IBcryptSenhaService bcryptSenhaService)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _bcryptSenhaService =  bcryptSenhaService;
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
            await ValidarDadosDoUsuario(usuario);
            usuario.Senha = _bcryptSenhaService.CriptografarSenha(usuario.Senha);

            await _usuarioRepositorio.AdicionarUsuarioAsync(usuario);
        }


        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            await ValidarDadosDoUsuario(usuario);
            await _usuarioRepositorio.AtualizarUsuarioAsync(usuario);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync()
        {
            return await _usuarioRepositorio.ObterUsuariosAsync();
        }

        public Task<Usuario> ObterUsuarioPorIdAsync(int id)
        {
           return _usuarioRepositorio.ObterUsuarioPorIdAsync(id);
        }

        public async Task RemoverUsuarioAsync(int id)
        {
            var usuarioDominio =await _usuarioRepositorio.ObterUsuarioPorIdAsync(id);
            if (usuarioDominio == null)
            {
                throw new ArgumentNullException("Usuário não encontrado");
            }
            await _usuarioRepositorio.AtualizarUsuarioAsync(usuarioDominio);
            
        }

        #region Métodos Privados
        private async Task ValidarDadosDoUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Usuario não pode ser nulo");
            }

            await VerificarSeUsuarioExiste(usuario);



            if (string.IsNullOrEmpty(usuario.Nome))
            {
                throw new ArgumentNullException("Nome não pode ser nulo");
            }
            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new ArgumentNullException("Email não pode ser nulo");
            }
            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new ArgumentNullException("Senha não pode ser nulo");
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