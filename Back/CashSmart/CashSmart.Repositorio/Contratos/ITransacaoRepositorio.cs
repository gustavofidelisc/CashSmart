using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Models;

namespace CashSmart.Repositorio.Contratos
{
    public interface ITransacaoRepositorio
    {
        public Task<int> AdicionarTransacaoAsync(Transacao transacao);
        public Task<IEnumerable<Transacao>> obterTodasTransacoesUsuarioAsync(Guid usuarioId);
        public Task<IEnumerable<Transacao>> obterTodasTransacoesUsuarioPorDataAsync(Guid usuarioId, DateTime dataInicial, DateTime dataFinal);

        public Task<Transacao> obterTransacaoPorUsuarioAsync(int id,Guid usuarioId);

        public Task AtualizarTransacaoAsync(Transacao transacao);

        public Task RemoverTransacaoAsync(Transacao transacao);
        public Task<TransacaoInformacoes> obterInformacoesTransacoesPorData(Guid usuarioId, DateTime  dataIncial, DateTime  dataFinal);
        public Task<SaldoUsuario> obterSaldoUsuario(Guid usuarioId, DateTime dataFinal);

        public Task<GraficoInformacoes> obterInformacoesGraficoPelaCategoria (Guid usuarioId, DateTime dataInicial, DateTime dataFinal, int tipoTransacaoId);
        public Task<GraficoInformacoesAnuais> obterInformacoesTransacoesPorAno(Guid usuarioId, int ano);

    }
}