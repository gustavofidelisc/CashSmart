namespace CashSmart.Repositorio.Models
{
    public class TransacaoInformacoesResposta
    {
        public Guid ID_USUARIO { get; set; }
        public decimal TOTAL_RECEITA { get; set; }
        public decimal TOTAL_DESPESA { get; set; }
    }
}