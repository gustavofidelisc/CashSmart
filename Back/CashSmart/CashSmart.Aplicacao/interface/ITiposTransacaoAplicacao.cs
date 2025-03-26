using CashSmart.Dominio.Enumeradores;

namespace CashSmart.Aplicacao.Interface
{
    public interface ITiposTransacaoAplicacao 
    {
        List<TipoDaTransacao> ListarTiposTransacao();
    }
}