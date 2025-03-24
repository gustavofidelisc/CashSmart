using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IFormaPagamentoRepositorio
    {
        public Task<int> AdicionarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int id , bool ativo);
        public Task<IEnumerable<FormaPagamento>> ObterFormasPagamentoAsync(bool ativo);
        public Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task<FormaPagamento> ObterFormaPagamentoPorNomeAsync(string query);
    }
}