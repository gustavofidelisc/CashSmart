namespace CashSmart.Dominio.Entidades
{
    public class Usuario: EntidadeBase
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public List<Transacao> Transasoes { get; set; }
        public Usuario() : base()
        {
        }

    }
}