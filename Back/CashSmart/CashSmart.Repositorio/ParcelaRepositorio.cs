using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using Microsoft.EntityFrameworkCore;

namespace CashSmart.Repositorio
{
    public class ParcelaRepositorio : BaseRepositorio, IParcelaRepositorio
    {
        public ParcelaRepositorio(CashSmartContexto context) : base(context)
        {
        }

        public async Task AdicionarParcelaAsync(Parcela parcela)
        {
            await _context.Parcelas.AddAsync(parcela);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarParcelaAsync(Parcela parcela)
        {
            _context.Parcelas.Update(parcela);
            await _context.SaveChangesAsync();
        }


        public async Task<Parcela> ObterParcelaPorIdAsync(int id)
        {
            return await _context.Parcelas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Parcela>> ObterTodasParcelasAsync()
        {
            return await _context.Parcelas.ToListAsync();
        }
    }
}