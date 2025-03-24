using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio
{
    public interface ICategoriaRepositorio
    {
        public Task<int> AdicionarCategoriaAsync(Categoria categoria);
        public Task<Categoria> ObterCategoriaPorIdAsync(int id , bool ativo);
        public Task<IEnumerable<Categoria>> ObterCategoriasAsync(bool ativo);
        public Task AtualizarCategoriaAsync(Categoria categoria);
        public Task<Categoria> ObterCategoriaPorNomeAsync(string query);
    }
}
