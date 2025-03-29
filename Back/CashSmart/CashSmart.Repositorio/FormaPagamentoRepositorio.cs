using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using Microsoft.EntityFrameworkCore;

namespace CashSmart.Repositorio
{
    public class FormaPagamentoRepositorio : BaseRepositorio, IFormaPagamentoRepositorio
    {

        public FormaPagamentoRepositorio(CashSmartContexto contexto) : base(contexto)
        {
            
        }
        public async Task<int> AdicionarFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            await _context.FormasPagamento.AddAsync(formaPagamento);
            await _context.SaveChangesAsync();
            return formaPagamento.Id;
        }

        public async Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            _context.FormasPagamento.Update(formaPagamento);
            await _context.SaveChangesAsync();
        }

        public async Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int id, Guid usuarioId)
        {
            return await _context.FormasPagamento.FirstOrDefaultAsync(f => f.Id == id && f.UsuarioId == usuarioId);
        }

        public async Task<FormaPagamento> ObterFormaPagamentoPorNomeAsync(string query, Guid usuarioId)
        {
            return await _context.FormasPagamento.FirstOrDefaultAsync(f => f.Nome.Contains(query) && f.UsuarioId == usuarioId);
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormasPagamentoAsync(Guid usuarioId)
        {
            return await _context.FormasPagamento.Where(f => f.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task RemoverFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            _context.FormasPagamento.Remove(formaPagamento);
            await _context.SaveChangesAsync();
        }
    }
}