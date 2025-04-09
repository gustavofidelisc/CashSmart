namespace CashSmart.Repositorio.Models
{
    public class GraficoInformacoesAnuais
    {
        public decimal[] Saldos { get; set; } 
        public string[] Meses { get; set; }
        public decimal[] Receitas { get; set; }
        public decimal[] Despesas { get; set; }     
        
    }
}