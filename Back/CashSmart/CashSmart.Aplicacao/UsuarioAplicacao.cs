using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contratos;

namespace CashSmart.Aplicacao
{
    public class UsuarioAplicacao : IUsuarioAplicacao
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioAplicacao(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task AdicionarUsuarioAsync(Usuario usuario)
        {
            ValidarDadosDoUsuario(usuario);

            await _usuarioRepositorio.AdicionarUsuarioAsync(usuario);
        }


        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            ValidarDadosDoUsuario(usuario);
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

        public async Task RemoverUsuarioAsync(Usuario usuario)
        {
            await _usuarioRepositorio.DeletarUsuarioAsync(usuario);
        }

        #region Métodos Privados
        private async void ValidarDadosDoUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException("Usuario não pode ser nulo");
            }
            

            var usuarioRepositorio = await _usuarioRepositorio.ObterUsuarioPorEmailAsync(usuario.Email);
            if (usuarioRepositorio != null)
            {
                throw new ArgumentException("Email já cadastrado");
            }

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
        #endregion
    }
}