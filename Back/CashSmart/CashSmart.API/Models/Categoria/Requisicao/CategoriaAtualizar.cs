namespace CashSmart.API.Models.Requisicao
{
    public class CategoriaAtualizar
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int TipoTransacao { get; set; }
    }
}