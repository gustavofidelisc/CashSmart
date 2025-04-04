using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio
{
    public interface ICategoriaRepositorio
    {
        public Task<int> AdicionarCategoriaAsync(Categoria categoria);
        public Task<Categoria> ObterCategoriaPorIdAsync(int id);
        public Task<IEnumerable<Categoria>> ObterTodasCategoriasUsuarioAsync(Guid usuarioId);
        public Task AtualizarCategoriaAsync(Categoria categoria);
        public Task<Categoria> ObterCategoriaPorNomeAsync(string query);
        public Task RemoverCategoriaAsync(int categoriaId, Guid usuarioId);
    }
}
