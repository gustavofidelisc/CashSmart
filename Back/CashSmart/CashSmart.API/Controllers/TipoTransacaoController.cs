using CashSmart.API.Models.TipoTransacao.Resposta;
using CashSmart.Aplicacao.Interface;
using CashSmart.Dominio.Enumeradores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashSmart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoTransacaoController : ControllerBase
    {
        public ITiposTransacaoAplicacao _tiposTransacaoAplicacao { get; set; }

        public TipoTransacaoController(ITiposTransacaoAplicacao tiposTransacaoAplicacao)
        {
            _tiposTransacaoAplicacao = tiposTransacaoAplicacao;
        }

        [Authorize]
        [HttpGet]
        [Route("Listar")]
        public IActionResult ListarTiposTransacao()
        {

            var tiposTransacoes = _tiposTransacaoAplicacao.ListarTiposTransacao();

            var tiposTransacoesResposta = new List<TipoTransacaoResposta>();

            foreach (var item in tiposTransacoes)
            {
                tiposTransacoesResposta.Add(new TipoTransacaoResposta
                {
                    Id = (int)item,
                    Nome = item.ToString()
                });
            } 
            return Ok(tiposTransacoesResposta);
        }
    }
}