using CashSmart.Dominio.Entidades;

namespace CashSmart.Dominio.Entidades
{
    class Parcela 
    {
        public int Id { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public int NumeroDaParcela { get; set; }
        public int TransacaoID { get; set; }
        public Transacao Transacao { get; set; }
    }
}