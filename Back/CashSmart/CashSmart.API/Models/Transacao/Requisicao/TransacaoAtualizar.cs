namespace CashSmart.API.Models.Transacao.Requisicao
{
    public class TransacaoAtualizar
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public DateTime DataTransacao { get; set; }
        public string Descricao { get; set; }
        public int FormaPagamentoId { get; set; }
        public decimal Valor { get; set; }      
    }
}