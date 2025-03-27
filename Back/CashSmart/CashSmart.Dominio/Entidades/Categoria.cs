namespace CashSmart.Dominio.Entidades
{
    public class Categoria : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public int TipoTransacao { get; set; }

        public List<Transacao> Transacoes { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Categoria() : base()
        {
            
        }    
    }

}
