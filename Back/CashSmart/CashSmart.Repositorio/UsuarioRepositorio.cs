using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using Microsoft.EntityFrameworkCore;
namespace CashSmart.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(CashSmartContexto contexto) : base(contexto)
        {
        }

        public async Task<int> AdicionarUsuarioAsync(Usuario usuario){
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario.Id;
        }

        public async Task<Usuario> ObterUsuarioPorIdAsync(int id, bool ativo){
            return await _context.Usuarios.Where(u=> u.Ativo == ativo).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterUsuariosAsync(bool ativo ){
            return await _context.Usuarios.Where(u=> u.Ativo == ativo).ToListAsync();
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario){
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarUsuarioAsync(Usuario usuario){
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ObterUsuarioPorEmailAsync(string email){
            return await _context.Usuarios.Where(u => u.Ativo).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}