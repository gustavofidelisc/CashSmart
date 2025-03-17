using CashSmart.Dominio.Entidades;

namespace CashSmart.Aplicacao.Interface
{
    public interface IUsuarioAplicacao
    {
        Task AdicionarUsuarioAsync(Usuario usuario);
        Task<Usuario> ObterUsuarioPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ObterTodosUsuariosAsync();
        Task AtualizarUsuarioAsync(Usuario usuario);
        Task RemoverUsuarioAsync(Usuario usuario);
    }
}