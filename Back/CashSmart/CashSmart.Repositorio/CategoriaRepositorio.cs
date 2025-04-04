using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;
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


        public async Task<Categoria> ObterCategoriaPorIdAsync(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Categoria> ObterCategoriaPorNomeAsync(string query)
        {
            return _context.Categorias.Where(c=> c.Nome.Contains(query)).FirstOrDefaultAsync(c => c.Nome == query);
        }

        public async Task<IEnumerable<Categoria>> ObterTodasCategoriasUsuarioAsync(Guid usuarioId)
        {
            return await _context.Categorias.Where(c => c.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task RemoverCategoriaAsync(int categoriaId, Guid usuarioId)
        {
            try{
                await _context.Database.GetDbConnection().ExecuteAsync
                ("SP_DELETAR_CATEGORIA_E_TRANSACOES", new { 
                    ID_CATEGORIA = categoriaId,
                    ID_USUARIO = usuarioId
                    }, commandType: CommandType.StoredProcedure);
            }catch (Exception ex){
                throw new Exception("Erro ao remover categoria" +ex.Message, ex);
            }
            
        }
    }
}