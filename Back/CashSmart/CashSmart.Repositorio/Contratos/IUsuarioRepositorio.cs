using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IUsuarioRepositorio
    {
        public Task<int> AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> ObterUsuarioPorIdAsync(int id , bool ativo);

        public Task<IEnumerable<Usuario>> ObterUsuariosAsync(bool ativo);

        public Task AtualizarUsuarioAsync(Usuario usuario);

        public Task DeletarUsuarioAsync(Usuario usuario);

        public Task<Usuario> ObterUsuarioPorEmailAsync(string email);

    }
}