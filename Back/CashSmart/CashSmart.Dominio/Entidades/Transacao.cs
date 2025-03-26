using CashSmart.Dominio.Entidades;
namespace CashSmart.Dominio.Entidades
{
    public class Transacao : EntidadeBase
    {
        public decimal Valor {get;set;}
        public string Descricao {get;set;}
        public DateTime Data {get;set;}
        public int FormaPagamentoId {get;set;}
        public FormaPagamento FormaPagamento {get;set;}
        public int CategoriaId {get;set;}
        public Categoria Categoria {get;set;}        
        public Guid UsuarioId {get;set;}
        public Usuario Usuario {get;set;}  

        public List<Parcela> Parcelas {get;set;} 


    }
}
