using System.Data.SqlTypes;
using CashSmart.API.Models.Exceptions;
using CashSmart.API.Models.Requisicao;
using CashSmart.API.Models.Transacao.Requisicao;
using CashSmart.API.Models.Transacao.Resposta;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using CashSmart.Dominio.Enumeradores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {
        ITransacaoAplicacao _transacaoAplicacao;
        public TransacaoController(ITransacaoAplicacao transacaoAplicacao)
        {
            _transacaoAplicacao = transacaoAplicacao;
        }
        [Authorize]
        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarTransacao([FromBody] TransacaoCriar transacao)
        {
            try
            {
                if (transacao == null)
                {
                    throw new ArgumentNullException("Transação não pode ser nula.");
                }
                var transacaoDominio = new Transacao{
                    CategoriaId = transacao.CategoriaId,
                    Data = transacao.DataTransacao,
                    Descricao = transacao.Descricao,
                    FormaPagamentoId = transacao.FormaPagamentoId,
                    Valor = transacao.Valor,
                    UsuarioId = this.ObterUsuarioIdDoHeader()

                };
                var transacaoId = await _transacaoAplicacao.AdicionarTrasacaoAsync(transacaoDominio);
                return Ok(transacaoId);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
            catch (SqlNullValueException ex)
            {
                return BadRequest(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("listar")]
        public async Task<IActionResult> ListarTodasTransacoes()
        {
            try
            {
                var transacoes = await _transacaoAplicacao.ObterTransacoesUsuarioAsync(this.ObterUsuarioIdDoHeader());

                var transacoesResposta = transacoes.Select(item => new TransacaoResposta
                {
                    Id = item.Id,
                    Data = item.Data,
                    Descricao = item.Descricao,
                    Valor = item.Valor,
                    nomeCategoria = item.Categoria.Nome,
                    nomeFormaPagamento = item.FormaPagamento.Nome,
                    TipoTransacao = Enum.GetName(typeof(TipoDaTransacao), item.Categoria.TipoTransacao)
                });
                return Ok(transacoesResposta);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
        }   

        [Authorize]
        [HttpGet("listar/{id}")]
        public async Task<IActionResult> ListarTransacaoPorId([FromRoute] int id)
        {
            try
            {
                var transacao = await _transacaoAplicacao.ObterTransacaoPorUsuarioAsync(id, this.ObterUsuarioIdDoHeader());
                var transacaoResposta = new TransacaoResposta {
                    Data = transacao.Data,
                    Descricao = transacao.Descricao,
                    Id = transacao.Id,
                    Valor = transacao.Valor,
                    nomeCategoria = transacao.Categoria.Nome,
                    nomeFormaPagamento = transacao.FormaPagamento.Nome,
                    TipoTransacao = Enum.GetName(typeof(TipoDaTransacao), transacao.Categoria.TipoTransacao)
                };

                return Ok(transacaoResposta);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarTransacaoAsync( [FromBody] TransacaoAtualizar transacao){
            try
            {
                var transacaoAtualizar = new Transacao{
                    Id = transacao.Id,
                    UsuarioId = this.ObterUsuarioIdDoHeader(),
                    CategoriaId = transacao.CategoriaId,
                    Data = transacao.DataTransacao,
                    DataAtualizacao = DateTime.Now,
                    FormaPagamentoId = transacao.FormaPagamentoId,
                    Descricao = transacao.Descricao,
                    Valor = transacao.Valor,
                };
                await _transacaoAplicacao.AtualizarTransacaoAsync(transacaoAtualizar, this.ObterUsuarioIdDoHeader());
                return Ok();
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta{
                    Mensagem = ex.Message
                });
            }
        }


    }
}