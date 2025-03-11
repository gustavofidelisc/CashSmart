using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IUsuarioRepositorio
    {
        public Task<int> AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> ObterUsuarioPorIdAsync(int id);

        public Task<IEnumerable<Usuario>> ObterUsuariosAsync();

        public Task AtualizarUsuarioAsync(Usuario usuario);

        public Task DeletarUsuarioAsync(Usuario usuario);

    }
}