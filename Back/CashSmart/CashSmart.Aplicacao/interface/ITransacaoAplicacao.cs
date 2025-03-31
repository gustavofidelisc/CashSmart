using CashSmart.Dominio.Entidades;

namespace CashSmart.Aplicacao.Interface
{
    public interface ITransacaoAplicacao
    {
        public  Task<int> AdicionarTrasacaoAsync (Transacao transacao);

        public Task<IEnumerable<Transacao>> ObterTransacoesUsuarioAsync(Guid usuarioId) ;
        public Task<Transacao> ObterTransacaoPorUsuarioAsync(int id, Guid usuarioId);

        public Task AtualizarTransacaoAsync(Transacao transacao, Guid usuarioId);
    }
}