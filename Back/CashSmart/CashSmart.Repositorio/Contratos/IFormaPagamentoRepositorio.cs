using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface IFormaPagamentoRepositorio
    {
        public Task<int> AdicionarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int id, Guid usuarioId);
        public Task<IEnumerable<FormaPagamento>> ObterFormasPagamentoAsync(Guid usuarioId);
        public Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento);
        public Task<FormaPagamento> ObterFormaPagamentoPorNomeAsync(string query, Guid usuarioId);
        public Task RemoverFormaPagamentoAsync(FormaPagamento formaPagamento);
    }
}