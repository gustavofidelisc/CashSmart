using System.Data.SqlTypes;
using CashSmart.API.Models.Exceptions;
using CashSmart.API.Models.FormaPagamento.Requisicao;
using CashSmart.API.Models.FormaPagamento.Resposta;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagamentoController : ControllerBase
    {
        private readonly IFormaPagamentoAplicacao _formaPagamentoAplicacao;
        private readonly ITiposTransacaoAplicacao _tiposTransacaoAplicacao;

        public FormaPagamentoController(IFormaPagamentoAplicacao formaPagamentoAplicacao, ITiposTransacaoAplicacao tiposTransacaoAplicacao)
        {
            _formaPagamentoAplicacao = formaPagamentoAplicacao;
        }

        [Authorize]
        [HttpPost("Criar")]
        public async Task<IActionResult> AdicionarFormaPagamento([FromBody] FormaPagamentoCriar formaPagamento)
        {
            try
            {
                var formaPagamentoDominio = new FormaPagamento {
                    Nome = formaPagamento.Nome,
                    UsuarioId = this.ObterUsuarioIdDoHeader()
                };

                var idFormaPagamento = await _formaPagamentoAplicacao.AdicionarFormaPagamentoAsync(formaPagamentoDominio);
                return Ok(idFormaPagamento);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (SqlNullValueException
             ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new  ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }

        [Authorize]
        [HttpGet("Listar")]
        public async Task<IActionResult> ListarTiposTransacoes()
        {
            try
            {
                var formaPagamento = await  _formaPagamentoAplicacao.ObterFormasPagamentoAsync(this.ObterUsuarioIdDoHeader());

                var formaPagamentoResposta = formaPagamento.Select(item => new FormaPagamentoResposta
                {
                    Id = item.Id,
                    Nome = item.Nome,
                });
                return Ok(formaPagamentoResposta);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }

        }

        [Authorize]
        [HttpGet("ObterPorId")]
        public async Task<IActionResult> ObterFormaPagamentoPorId([FromQuery] int id)
        {
            try
            {
                var formaPagamento = await _formaPagamentoAplicacao.ObterFormaPagamentoPorIdAsync(id, this.ObterUsuarioIdDoHeader());

                var formaPagamentoResposta = new FormaPagamentoResposta
                {
                    Id = formaPagamento.Id,
                    Nome = formaPagamento.Nome,
                };
                return Ok(formaPagamentoResposta);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }


        [Authorize]
        [HttpPut("Atualizar")]
        public async Task<IActionResult> AtualizarFormaPagamento([FromBody] FormaPagamentoAtualizar formaPagamento)
        {
            try
            {
                var formaPagamentoDominio = new FormaPagamento
                {
                    Id = formaPagamento.Id,
                    Nome = formaPagamento.Nome,
                    UsuarioId = this.ObterUsuarioIdDoHeader()
                };

                await _formaPagamentoAplicacao.AtualizarFormaPagamentoAsync(formaPagamentoDominio);
                return Ok(formaPagamento);
            }
            catch (SqlNullValueException ex)
            {
                return NotFound(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResposta
                {
                    Mensagem = ex.Message
                });
            }
        }

    }
}