namespace CashSmart.Repositorio.Models
{
    
    public class GraficoInformacoesAnuaisReposta
    {
        public decimal Saldo { get; set; } 
        public List<string> Meses { get; set; }
        public List<decimal> Receitas { get; set; }
        public List<decimal> Despesas { get; set; }     
        
    }
}