using System.Data.SqlTypes;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Dominio.Enumeradores;
using CashSmart.Repositorio;
using CashSmart.Repositorio.Contratos;

namespace CashSmart.Aplicacao
{
    public class TransacaoAplicacao: ITransacaoAplicacao
    {
        ITransacaoRepositorio _transacaoRepositorio;
        IFormaPagamentoRepositorio _formaPagamentoRepositorio;
        ICategoriaRepositorio _categoriaRepositorio;
        public TransacaoAplicacao(
            ITransacaoRepositorio transacaoRepositorio,
            IFormaPagamentoRepositorio formaPagamentoRepositorio,
            ICategoriaRepositorio categoriaRepositorio)
        {
            _transacaoRepositorio = transacaoRepositorio;
            _formaPagamentoRepositorio = formaPagamentoRepositorio;
            _categoriaRepositorio = categoriaRepositorio;
        }

        public async Task<int> AdicionarTrasacaoAsync (Transacao transacao){
            try
            {
                var categoria = await _categoriaRepositorio.ObterCategoriaPorIdAsync(transacao.CategoriaId);

                if (categoria == null)
                {
                    throw new SqlNullValueException("Categoria não encontrada.");
                }

                await VerificarTransacao(transacao);

                if (categoria.TipoTransacao == (int)TipoDaTransacao.Despesa){
                    transacao.Valor *= -1;
                }

                var transacaoId = await _transacaoRepositorio.AdicionarTransacaoAsync(transacao);
                return transacaoId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Transacao>> ObterTransacoesUsuarioAsync(Guid usuarioId) {
            var transacoes = await _transacaoRepositorio.obterTodasTransacoesUsuarioAsync(usuarioId);
            if (transacoes == null)
            {
                throw new SqlNullValueException("Transações não encontradas.");
            }
            return transacoes;
        }

        public async Task<Transacao> ObterTransacaoPorUsuarioAsync(int id, Guid usuarioId) {
            var transacao = await _transacaoRepositorio.obterTransacaoPorUsuarioAsync(id, usuarioId);
            if (transacao == null)
            {
                throw new SqlNullValueException("Transacao não encontrada.");
            }
            return transacao;
        }


        public async Task AtualizarTransacaoAsync(Transacao transacao, Guid usuarioId) {
            try
            {
                var transacaoDominio = await _transacaoRepositorio.obterTransacaoPorUsuarioAsync(transacao.Id, transacao.UsuarioId);
                if (transacaoDominio == null)
                {
                    throw new SqlNullValueException("Transação não encontrada.");
                }

                if (transacao.UsuarioId != usuarioId)
                {
                    throw new SqlNullValueException("Transação não encontrada.");
                }
                var categoria = await _categoriaRepositorio.ObterCategoriaPorIdAsync(transacao.CategoriaId);
                if (categoria == null)
                {
                    throw new SqlNullValueException("Categoria não encontrada.");
                }
                await VerificarTransacao(transacao);

                if (categoria.TipoTransacao == (int)TipoDaTransacao.Despesa){
                    transacao.Valor *= -1;
                }

                transacaoDominio.CategoriaId = transacao.CategoriaId;
                transacaoDominio.DataAtualizacao = transacao.DataAtualizacao;
                transacaoDominio.Descricao = transacao.Descricao;
                transacaoDominio.FormaPagamentoId = transacao.FormaPagamentoId;
                transacaoDominio.Data = transacao.Data;
                transacaoDominio.Valor = transacao.Valor;

                await _transacaoRepositorio.AtualizarTransacaoAsync(transacaoDominio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region 

        private async Task VerificarTransacao(Transacao transacao)
        {
            var formaPagamento = await _formaPagamentoRepositorio.ObterFormaPagamentoPorIdAsync(transacao.FormaPagamentoId, transacao.UsuarioId);
            if (formaPagamento == null)
            {
                throw new SqlNullValueException("Forma de pagamento não encontrada.");
            }

            if(transacao.Valor <= 0)
            {
                throw new SqlNullValueException("Valor inválido.");
            }
            if(transacao.Data == DateTime.MinValue)
            {
                throw new SqlNullValueException("Data inválida.");
            }


        }


        #endregion

    }
}