using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IUsuarioRepositorio
    {
        public Task<Guid> AdicionarUsuarioAsync(Usuario usuario);
        public Task<Usuario> ObterUsuarioPorIdAsync(Guid id , bool ativo);

        public Task<IEnumerable<Usuario>> ObterUsuariosAsync(bool ativo);

        public Task AtualizarUsuarioAsync(Usuario usuario);

        public Task<Usuario> ObterUsuarioPorEmailAsync(string email);

    }
}