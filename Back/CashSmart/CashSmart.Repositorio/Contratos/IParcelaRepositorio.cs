using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IParcelaRepositorio
    {
        public Task AdicionarParcelaAsync(Parcela parcela);
        public Task AtualizarParcelaAsync(Parcela parcela);

        public Task<Parcela> ObterParcelaPorIdAsync(int id);

        public Task<IEnumerable<Parcela>> ObterTodasParcelasAsync();
    }       
}