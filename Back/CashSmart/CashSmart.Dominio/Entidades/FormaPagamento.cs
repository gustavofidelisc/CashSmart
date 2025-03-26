namespace CashSmart.Dominio.Entidades
{
    public class FormaPagamento : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;

        public List<Transacao> Transacoes { get; set; }
        public FormaPagamento() : base()
        {
            
        }
    }
    }
