using System.Data.SqlTypes;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contratos;

namespace CashSmart.Aplicacao
{
    public class FormaPagamentoAplicacao : IFormaPagamentoAplicacao
    {
        private IFormaPagamentoRepositorio  _formaPagamentoRepositorio;
        public FormaPagamentoAplicacao(IFormaPagamentoRepositorio formaPagamentoRepositorio)
        {
            _formaPagamentoRepositorio = formaPagamentoRepositorio;
        }

        public async Task<int> AdicionarFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            VerificarFormaPagamento(formaPagamento);
            return await _formaPagamentoRepositorio.AdicionarFormaPagamentoAsync(formaPagamento);
        }

        public async Task AtualizarFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            try
            {
                var formaPagamentoDominio = await ObterFormaPagamentoPorIdAsync(formaPagamento.Id, formaPagamento.UsuarioId);
                VerificarFormaPagamento(formaPagamento);
                formaPagamentoDominio.Nome = formaPagamento.Nome;
                await _formaPagamentoRepositorio.AtualizarFormaPagamentoAsync(formaPagamentoDominio);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FormaPagamento> ObterFormaPagamentoPorIdAsync(int id, Guid usuarioId)
        {
            var formaPagamentoDominio =  await _formaPagamentoRepositorio.ObterFormaPagamentoPorIdAsync(id, usuarioId);
            if (formaPagamentoDominio == null)
            {
                throw new SqlNullValueException("Forma de pagamento não encontrada.");
            }
            return formaPagamentoDominio;
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormasPagamentoAsync(Guid usuarioId)
        {
            return await _formaPagamentoRepositorio.ObterFormasPagamentoAsync(usuarioId);
        }
        public async Task<FormaPagamento> ObterFormaPagamentoPorNomeAsync(string query, Guid usuarioId)
        {
            var formaPagamentoDominio = await _formaPagamentoRepositorio.ObterFormaPagamentoPorNomeAsync(query, usuarioId);
            if (formaPagamentoDominio == null)
            {
                throw new SqlNullValueException("Forma de pagamento não encontrada.");
            }
            return formaPagamentoDominio;
        }

        public async Task RemoverFormaPagamentoAsync(FormaPagamento formaPagamento)
        {
            if (formaPagamento == null)
            {
                throw new ArgumentNullException("Forma de pagamento não pode ser nula.");
            }

            await _formaPagamentoRepositorio.ObterFormaPagamentoPorIdAsync(formaPagamento.Id, formaPagamento.UsuarioId);
            if (formaPagamento.Id == 0)
            {
                throw new ArgumentException("Forma de pagamento não pode ser nula.");
            }
            await _formaPagamentoRepositorio.RemoverFormaPagamentoAsync(formaPagamento);
        }

        #region 

        private void VerificarFormaPagamento(FormaPagamento formaPagamento)
        {
            if (string.IsNullOrEmpty(formaPagamento.Nome))
            {
                throw new ArgumentException("Nome da forma de pagamento não pode ser nulo ou vazio.");
            }

            if (formaPagamento.UsuarioId == Guid.Empty)
            {
                throw new ArgumentException("Usuário não pode ser nulo ou vazio.");
            }


        }
        #endregion
    }
}