using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using Microsoft.EntityFrameworkCore;

namespace CashSmart.Repositorio
{
    public class TransacaoRepositorio : BaseRepositorio, ITransacaoRepositorio
    {
        public TransacaoRepositorio(CashSmartContexto context) : base(context)
        {
        }

        public async Task<int> AdicionarTransacaoAsync(Transacao transacao){
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
            return transacao.Id;
        }

        public async Task<IEnumerable<Transacao>> obterTodasTransacoesUsuarioAsync(Guid usuarioId){
            return await _context.Transacoes.Include(t => t.Categoria)
                    .Include(t => t.FormaPagamento)
                    .Where(t => t.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<Transacao> obterTransacaoPorUsuarioAsync(int id, Guid usuarioId){
            return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.FormaPagamento)
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);
        }

        public async Task AtualizarTransacaoAsync(Transacao transacao){
            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverTransacaoAsync(Transacao transacao){
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }
    }
}