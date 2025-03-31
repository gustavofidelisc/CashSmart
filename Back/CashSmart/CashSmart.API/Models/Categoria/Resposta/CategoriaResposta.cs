namespace CashSmart.API.Models.Resposta
{
    public class CategoriaResposta
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string TipoTransacao { get; set; }
    }
}