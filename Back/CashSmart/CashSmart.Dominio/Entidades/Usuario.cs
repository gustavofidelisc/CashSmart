namespace CashSmart.Dominio.Entidades
{
    public class Usuario: EntidadeBase
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public bool Ativo { get; set; }

        public List<Categoria> Categorias { get; set; }
        public List<FormaPagamento> FormasPagamento { get; set; }

        public List<Transacao> Transacoes { get; set; }
        public Usuario() : base()
        {
            Ativo = true;
        }

    }
}