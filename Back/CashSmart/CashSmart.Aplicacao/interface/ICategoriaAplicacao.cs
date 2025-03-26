using CashSmart.Dominio.Entidades;

namespace Name
{
    public interface ICategoriaAplicacao
    {
        public Task<int> AdicionarCategoriaAsync(Categoria categoria);

        public Task AtualizarCategoriaAsync(Categoria categoria);

        public Task<IEnumerable<Categoria>> ObterTodasCategoriasAsync();

        public Task<Categoria> ObterCategoriaPorIdAsync(int id);
        public Task RemoverCategoriaAsync(int id);
    }
}