using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Models;

namespace CashSmart.Aplicacao.Interface
{
    public interface ITransacaoAplicacao
    {
        public  Task<int> AdicionarTrasacaoAsync (Transacao transacao);

        public Task<IEnumerable<Transacao>> ObterTransacoesUsuarioAsync(Guid usuarioId, DateTime dataInicial, DateTime dataFinal);
        public Task<Transacao> ObterTransacaoPorUsuarioAsync(int id, Guid usuarioId);

        public Task AtualizarTransacaoAsync(Transacao transacao, Guid usuarioId);
        public Task<TransacaoInformacoes> obterInformacoesTransacoesPorData(Guid usuarioId, DateTime  dataIncial, DateTime  dataFinal);
        public Task<SaldoUsuario> obterSaldoUsuario(Guid usuarioId, DateTime dataFinal);
        public Task<GraficoInformacoes> obterInformacoesGraficoPelaCategoria(Guid usuarioId, DateTime dataInicial, DateTime dataFinal, int tipoTransacaoId);


    }
}