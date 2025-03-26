using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IFormaPagamentoRepositorio
    {
        public Task<int> AdicionarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int id );
        public Task<IEnumerable<FormaPagamento>> ObterFormasPagamentoAsync();
        public Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task<FormaPagamento> ObterFormaPagamentoPorNomeAsync(string query);
    }
}