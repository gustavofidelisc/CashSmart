using CashSmart.Dominio.Entidades;

namespace Name
{
    public interface ICategoriaAplicacao
    {
        public Task<int> AdicionarCategoriaAsync(Categoria categoria);

        public Task AtualizarCategoriaAsync(Categoria categoria, Guid usuarioId);

        public Task<IEnumerable<Categoria>> ObterTodasCategoriasUsuarioAsync(Guid usuarioId);

        public Task<Categoria> ObterCategoriaPorIdAsync(int id, Guid usuarioId);
        public Task RemoverCategoriaAsync(int id, Guid usuarioId);
    }
}