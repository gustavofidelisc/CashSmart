using CashSmart.Dominio.Entidades;

namespace CashSmart.Aplicacao.Interface
{
    public interface IUsuarioAplicacao
    {
        Task<Guid> AdicionarUsuarioAsync(Usuario usuario);
        Task<Usuario> ObterUsuarioPorIdAsync(Guid id);
        Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync(bool ativo);
        public  Task<Usuario> ObterUsuarioPorEmailAsync(string email);

        Task AtualizarUsuarioAsync(Usuario usuario);
        Task RemoverUsuarioAsync(Guid id);
        Task RestaurarUsuarioAsync(Guid id);
    }
}