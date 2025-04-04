using CashSmart.Dominio.Entidades;

namespace CashSmart.Aplicacao.Interface
{
    public interface IFormaPagamentoAplicacao
    {
        Task<int> AdicionarFormaPagamentoAsync(FormaPagamento formaPagamento);
        Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento);
        Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int id, Guid usuarioId);
        Task<IEnumerable<FormaPagamento>> ObterFormasPagamentoAsync(Guid usuarioId);
        Task<FormaPagamento> ObterFormaPagamentoPorNomeAsync(string query, Guid usuarioId);
         public Task RemoverFormaPagamentoAsync(int formaPagamentoId, Guid usuarioId);

    }
}