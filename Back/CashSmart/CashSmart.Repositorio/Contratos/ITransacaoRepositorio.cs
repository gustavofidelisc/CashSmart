using CashSmart.Dominio.Entidades;

namespace CashSmart.Repositorio.Contratos
{
    public interface ITransacaoRepositorio
    {
        public Task<int> AdicionarTransacaoAsync(Transacao transacao);

        public Task<Transacao> obterTransacaoAsync(int id);

        public Task<IEnumerable<Transacao>> obterTodasTransacoesAsync();

        public Task<IEnumerable<Transacao>> obterTransacoesPorUsuarioAsync(Guid usuarioId);

        public Task<IEnumerable<Transacao>> obterTransacoesPorCategoriaAsync(int categoriaId);
        public Task<IEnumerable<Transacao>> obterTransacoesPorFormaPagamentoAsync(int formaPagamentoId);

        public Task<IEnumerable<Transacao>> obterTransacoesPorPeriodoAsync( DateTime dataInicial, DateTime dataFinal);

        public Task AtualizarTransacaoAsync(Transacao transacao);

        public Task RemoverTransacaoAsync(Transacao transacao);
    }
}