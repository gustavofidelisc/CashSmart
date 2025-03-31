namespace CashSmart.API.Models.Requisicao
{
    public class TransacaoCriar
    {
        public decimal Valor { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataTransacao { get; set; }
        public int CategoriaId { get; set; }
        public int FormaPagamentoId { get; set; }
    }
}