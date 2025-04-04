using System.Data.SqlTypes;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Dominio.Enumeradores;
using CashSmart.Repositorio;
using CashSmart.Repositorio.Contratos;
using CashSmart.Repositorio.Models;

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

        public async Task<IEnumerable<Transacao>> ObterTransacoesUsuarioAsync(Guid usuarioId, DateTime dataInicial, DateTime dataFinal) {
            if (dataInicial == DateTime.MinValue || dataFinal == DateTime.MinValue)
            {
                return  await _transacaoRepositorio.obterTodasTransacoesUsuarioAsync(usuarioId);;
            }
            if (dataInicial > dataFinal)
            {
                throw new SqlNullValueException("Data inicial não pode ser maior que a data final.");
            }
            var transacoes = await _transacaoRepositorio.obterTodasTransacoesUsuarioPorDataAsync(usuarioId, dataInicial, dataFinal);
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

        public async Task<bool> RemoverTransacaoAsync(int id, Guid usuarioId) {
            try
            {
                var transacao = await _transacaoRepositorio.obterTransacaoPorUsuarioAsync(id, usuarioId);
                if (transacao == null)
                {
                    throw new SqlNullValueException("Transação não encontrada.");
                }
                if (transacao.UsuarioId != usuarioId)
                {
                    throw new SqlNullValueException("Transação não encontrada.");
                }
                await _transacaoRepositorio.RemoverTransacaoAsync(transacao);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TransacaoInformacoes> obterInformacoesTransacoesPorData(Guid usuarioId, DateTime dataIncial, DateTime dataFinal){
            var informacoes = await _transacaoRepositorio.obterInformacoesTransacoesPorData(usuarioId, dataIncial, dataFinal);
            if (informacoes == null)
            {
                return new TransacaoInformacoes {
                    Receitas = 0,
                    Despesas = 0
                };
            }
            return informacoes;
        }
        public async Task<SaldoUsuario> obterSaldoUsuario(Guid usuarioId, DateTime dataFinal){
            var informacoes = await _transacaoRepositorio.obterSaldoUsuario(usuarioId, dataFinal);
            if (informacoes == null)
            {
                return new SaldoUsuario {
                    Saldo = 0
                };
            }
            return informacoes;
        }

        public async Task<GraficoInformacoes> obterInformacoesGraficoPelaCategoria(Guid usuarioId, DateTime dataInicial, DateTime dataFinal, int tipoTransacaoId){
            var informacoes = await _transacaoRepositorio.obterInformacoesGraficoPelaCategoria(usuarioId, dataInicial, dataFinal, tipoTransacaoId);
            if (informacoes == null)
            {
                return new GraficoInformacoes {
                    CategoriaNomes = new string[0],
                    Valores = new decimal[0],
                    TipoTransacao = new int[0]
                };
            }
            return informacoes;
        }
        #region 

        private async Task VerificarTransacao(Transacao transacao)
        {
            try{

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

            }catch (Exception ex)
            {
                throw ex;
            }


        }

        #endregion

    }
}