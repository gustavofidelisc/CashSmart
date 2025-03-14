using CashSmart.Dominio.Entidades;
namespace CashSmart.Dominio.Entidades
{
    public class Transacao : EntidadeBase
    {
        public decimal Valor {get;set;}
        public int Parcelas {get;set;}
        public int FormaPagamentoID {get;set;}
        public FormaPagamento FormaPagamento {get;set;}
        public int CategoriaID {get;set;}
        public Categoria Categoria {get;set;}        
        public int UsuarioID {get;set;}
        public Usuario Usuario {get;set;}
        public int TipoDaTransacaoID {get;set;}
        
    }
}
