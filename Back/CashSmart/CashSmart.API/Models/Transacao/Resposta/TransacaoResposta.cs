using CashSmart.Dominio.Entidades;

namespace CashSmart.API.Models.Transacao.Resposta
{
    public class TransacaoResposta
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string nomeFormaPagamento { get; set; } = string.Empty;
        public string nomeCategoria { get; set; } = string.Empty;
        public string TipoTransacao { get; set; } = string.Empty;

        public int FormaPagamentoId { get; set; }
        public int CategoriaId { get; set; }

    }
}