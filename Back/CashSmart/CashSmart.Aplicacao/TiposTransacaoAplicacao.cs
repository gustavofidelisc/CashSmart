using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Enumeradores;

namespace CashSmart.Aplicacao
{
    public class TiposTransacaoAplicacao : ITiposTransacaoAplicacao
    {
        public List<TipoDaTransacao> ListarTiposTransacao()
        {
            return Enum.GetValues(typeof(TipoDaTransacao)).Cast<TipoDaTransacao>().ToList();
        }
    }
}