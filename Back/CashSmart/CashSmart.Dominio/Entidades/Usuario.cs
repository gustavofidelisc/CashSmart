namespace CashSmart.Dominio.Entidades
{
    public class Usuario: EntidadeBase
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;

        public Usuario() : base()
        {
        }

    }
}