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

        public async Task<Transacao> obterTransacaoAsync(int id){
            return await _context.Transacoes.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transacao>> obterTodasTransacoesAsync(){
            return await _context.Transacoes.ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> obterTransacoesPorUsuarioAsync(Guid usuarioId){
            return await _context.Transacoes.Where(t => t.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> obterTransacoesPorCategoriaAsync(int categoriaId){
            return await _context.Transacoes.Where(t => t.CategoriaId == categoriaId).ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> obterTransacoesPorFormaPagamentoAsync(int formaPagamentoId){
            return await _context.Transacoes.Where(t => t.FormaPagamentoId == formaPagamentoId).ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> obterTransacoesPorPeriodoAsync( DateTime dataInicial, DateTime dataFinal){
            return await _context.Transacoes.Where( t => t.Data >= dataInicial && t.Data <= dataFinal).ToListAsync();
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