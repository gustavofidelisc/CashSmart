using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;

namespace CashSmart.Repositorio
{
    public class CategoriaRepositorio : BaseRepositorio, ICategoriaRepositorio
    {

        public CategoriaRepositorio(CashSmartContexto contexto) : base(contexto)
        {
            
        }
        public async Task<int> AdicionarCategoriaAsync(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria.Id;
        }

        public async Task AtualizarCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }


        public async Task<Categoria> ObterCategoriaPorIdAsync(int id, bool ativo)
        {
            return await _context.Categorias.Where(c => c.Ativo == ativo).FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Categoria> ObterCategoriaPorNomeAsync(string query)
        {
            return _context.Categorias.Where(c=> c.Ativo == true && c.Nome.Contains(query)).FirstOrDefaultAsync(c => c.Nome == query);
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasAsync(bool ativo)
        {
            return await _context.Categorias.Where(c => c.Ativo == ativo).ToListAsync();
        }
    }
}