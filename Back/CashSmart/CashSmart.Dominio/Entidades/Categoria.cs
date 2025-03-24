namespace CashSmart.Dominio.Entidades
{
    public class Categoria : EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public int TipoTransacao { get; set; }

        public Categoria() : base()
        {
            
        }    
    }

}
