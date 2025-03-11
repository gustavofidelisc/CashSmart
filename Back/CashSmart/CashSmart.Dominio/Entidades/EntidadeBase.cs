namespace CashSmart.Dominio.Entidades
{
    public class EntidadeBase
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public EntidadeBase()
        {
            Ativo = true;
            DataCriacao = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }
    }
}