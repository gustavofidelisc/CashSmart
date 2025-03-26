using CashSmart.Dominio.Entidades;

namespace CashSmart.Dominio.Entidades
{
    public class Parcela 
    {
        public int Id { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public int NumeroDaParcela { get; set; }
        public int TransacaoId { get; set; }
        public Transacao Transacao { get; set; }
    }
}