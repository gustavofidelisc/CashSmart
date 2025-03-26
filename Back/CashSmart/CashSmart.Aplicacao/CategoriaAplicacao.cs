using System.Data.SqlTypes;
using System.Linq;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio;
using Name;

namespace CashSmart.Aplicacao
{
    public class CategoriaAplicacao : ICategoriaAplicacao
    {
        private readonly ICategoriaRepositorio _categoriaRepositorio;
        private readonly ITiposTransacaoAplicacao _tiposTransacaoAplicacao;

        public CategoriaAplicacao(ICategoriaRepositorio categoriaRepositorio, ITiposTransacaoAplicacao tiposTransacaoAplicacao)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _tiposTransacaoAplicacao = tiposTransacaoAplicacao;
        }
        
        public async Task<int> AdicionarCategoriaAsync(Categoria categoria)
        {
            ValidarDadosCategoria(categoria);

            return await _categoriaRepositorio.AdicionarCategoriaAsync(categoria);
        }

        public async Task AtualizarCategoriaAsync(Categoria categoria)
        {
            try{
                
                ValidarDadosCategoria(categoria);

                var cartegoriaRepositorio =await ObterCategoriaPorIdAsync(categoria.Id);

                if (cartegoriaRepositorio == null)
                {
                    throw new SqlNullValueException("Categoria não encontrada");
                }

                cartegoriaRepositorio.Nome = categoria.Nome;
                cartegoriaRepositorio.TipoTransacao = categoria.TipoTransacao;

                await _categoriaRepositorio.AtualizarCategoriaAsync(categoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Categoria>> ObterTodasCategoriasAsync()
        {
            return await _categoriaRepositorio.ObterCategoriasAsync();
        }

        public async Task<Categoria> ObterCategoriaPorIdAsync(int id)
        {
            var cartegoriaRepositorio = await _categoriaRepositorio.ObterCategoriaPorIdAsync(id);
            if (cartegoriaRepositorio == null)
            {
                throw new SqlNullValueException("Categoria não encontrada");
            }
            return cartegoriaRepositorio;
        }

        public async Task RemoverCategoriaAsync(int id)
        {
            var categoria = await ObterCategoriaPorIdAsync(id);
            await _categoriaRepositorio.AtualizarCategoriaAsync(categoria);
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
        #endregion
    }
}