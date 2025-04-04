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
        [HttpPost("Criar")]
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
        [HttpGet("Listar")]
        public async Task<IActionResult> ListarTodasTransacoes([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
            try
            {
                var transacoes = await _transacaoAplicacao.ObterTransacoesUsuarioAsync(this.ObterUsuarioIdDoHeader(), dataInicial, dataFinal);

                var transacoesResposta = transacoes.Select(item => new TransacaoResposta
                {
                    Id = item.Id,
                    Data = item.Data.Date,
                    Descricao = item.Descricao,
                    Valor = item.Valor,
                    nomeCategoria = item.Categoria.Nome,
                    nomeFormaPagamento = item.FormaPagamento.Nome,
                    TipoTransacao = Enum.GetName(typeof(TipoDaTransacao), item.Categoria.TipoTransacao),
                    FormaPagamentoId = item.FormaPagamentoId,
                    CategoriaId = item.CategoriaId
                
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
        [HttpGet("Listar/{id}")]
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
        [HttpPut("Atualizar")]
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

        [Authorize]
        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> RemoverTransacao([FromRoute] int id)
        {
            try
            {
                var transacao = await _transacaoAplicacao.RemoverTransacaoAsync(id, this.ObterUsuarioIdDoHeader());
                if (transacao == false)
                {
                    return NotFound(new ExceptionResposta{
                        Mensagem = "Transação não encontrada."
                    });
                }
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

         [Authorize]
        [HttpGet("Informacoes")]
        public async Task<IActionResult> ObterInformacoesTransacoesPorData([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
            try
            {
                var informacoes = await _transacaoAplicacao.obterInformacoesTransacoesPorData(this.ObterUsuarioIdDoHeader(), dataInicial, dataFinal);
                return Ok(informacoes);
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
        [HttpGet("SaldoUsuario")]
        public async Task<IActionResult> ObterSaldoUsuario( [FromQuery] DateTime dataFinal)
        {
            try
            {
                var saldo = await _transacaoAplicacao.obterSaldoUsuario(this.ObterUsuarioIdDoHeader(), dataFinal);
                return Ok(saldo);
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
        [HttpGet("Informacoes/Grafico/Categoria")]
        public async Task<IActionResult> obterInformacoesGraficoPelaCategoria([FromQuery] DateTime dataInicial, [FromQuery]DateTime dataFinal, [FromQuery] int tipoTransacaoId){
            try
            {
                var informacoes = await _transacaoAplicacao.obterInformacoesGraficoPelaCategoria(this.ObterUsuarioIdDoHeader(), dataInicial, dataFinal, tipoTransacaoId);
                return Ok(informacoes);
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