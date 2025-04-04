using System.Data.SqlTypes;
using System.Linq;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio;
using CashSmart.Repositorio.Contratos;
using Name;

namespace CashSmart.Aplicacao
{
    public class CategoriaAplicacao : ICategoriaAplicacao
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        private readonly ITiposTransacaoAplicacao _tiposTransacaoAplicacao;

        public CategoriaAplicacao(
            ICategoriaRepositorio categoriaRepositorio, 
            ITiposTransacaoAplicacao tiposTransacaoAplicacao,
            IUsuarioAplicacao usuarioAplicacao)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _tiposTransacaoAplicacao = tiposTransacaoAplicacao;
            _usuarioAplicacao = usuarioAplicacao;
        }
        
        public async Task<int> AdicionarCategoriaAsync(Categoria categoria)
        {
            ValidarDadosCategoria(categoria);

            return await _categoriaRepositorio.AdicionarCategoriaAsync(categoria);
        }

        public async Task AtualizarCategoriaAsync(Categoria categoria, Guid usuarioId)
        {
            try{
                
                ValidarDadosCategoria(categoria);

                await ValidarUsuarioCategoria(categoria, usuarioId);

                var cartegoriaRepositorio =await ObterCategoriaPorIdAsync(categoria.Id, categoria.UsuarioId);

                if (cartegoriaRepositorio == null)
                {
                    throw new SqlNullValueException("Categoria não encontrada");
                }

                cartegoriaRepositorio.Nome = categoria.Nome;
                cartegoriaRepositorio.TipoTransacao = categoria.TipoTransacao;
                cartegoriaRepositorio.DataAtualizacao = DateTime.UtcNow;

                await _categoriaRepositorio.AtualizarCategoriaAsync(cartegoriaRepositorio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Categoria>> ObterTodasCategoriasUsuarioAsync(Guid usuarioId)
        {
            var usuario = await _usuarioAplicacao.ObterUsuarioPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new SqlNullValueException("Usuário não encontrado");
            }
            return await _categoriaRepositorio.ObterTodasCategoriasUsuarioAsync(usuario.Id);
        }

        public async Task<Categoria> ObterCategoriaPorIdAsync(int id, Guid usuarioId)
        {
            try
            {
                var categoria = await _categoriaRepositorio.ObterCategoriaPorIdAsync(id);
                if (categoria == null)
                {
                    throw new SqlNullValueException("Categoria não encontrada");
                }

                await ValidarUsuarioCategoria(categoria, usuarioId);
                return categoria;
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task RemoverCategoriaAsync(int id, Guid usuarioId)
        {
            try{
                var usuario = await _usuarioAplicacao.ObterUsuarioPorIdAsync(usuarioId);
                if (usuario == null)
                {
                    throw new SqlNullValueException("Usuário não encontrado");
                }
                var categoria = await _categoriaRepositorio.ObterCategoriaPorIdAsync(id);
                if (categoria == null)
                {
                    throw new SqlNullValueException("Categoria não encontrada");
                }
                
                await _categoriaRepositorio.RemoverCategoriaAsync(id, usuario.Id);
                }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Metodos Privados
        private void ValidarDadosCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentNullException("Nome da categoria não pode ser nulo ou vazio");
            }

            var tiposTransacao = _tiposTransacaoAplicacao.ListarTiposTransacao();

            if (!tiposTransacao.Any(t => (int)t == categoria.TipoTransacao))
            {
                throw new ArgumentNullException("Tipo de transação inválido");
            }
        }

        public async Task ValidarUsuarioCategoria(Categoria categoria, Guid usuarioId)
        {
            var usuario = await _usuarioAplicacao.ObterUsuarioPorIdAsync(usuarioId);
            if (usuario == null)
            {
                throw new SqlNullValueException("Usuário não encontrado");
            }
            if (categoria.UsuarioId != usuario.Id)
            {
                throw new ArgumentNullException("Categoria não encontrada");
            }
        }
        #endregion
    }
}