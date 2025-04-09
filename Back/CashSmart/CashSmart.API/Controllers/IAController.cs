using CashSmart.Aplicacao.Interface;
using CashSmart.Servicos.Services.IA;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashSmart.API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class IAController : ControllerBase
    {
        private readonly OpenAIService _openAIService;
        private readonly ITransacaoAplicacao _transacaoAplicacao;

        public IAController(OpenAIService openAIService, ITransacaoAplicacao transacaoAplicacao)

        {
            _openAIService = openAIService;
            _openAIService.InitializeClient();
            _transacaoAplicacao = transacaoAplicacao;

        }

        [HttpPost("Analise/IA")]
        [Authorize]
        public async Task<IActionResult> Chat([FromQuery] int ano)
        {
            try
            {
                var informacaoAnual = await _transacaoAplicacao.obterInformacoesTransacoesPorAno(this.ObterUsuarioIdDoHeader(),ano);
                //transaformar a informacaoAnual em string para enviar para o IA
                var despesas = informacaoAnual.Despesas;
                var receitas = informacaoAnual.Receitas;
                var saldos = informacaoAnual.Saldos;
                //transaformar a informacaoAnual em string para enviar para o IA
                var informacaoAnualString = string.Empty;
                for (int i = 0; i < despesas.Length; i++)
                {
                    informacaoAnualString += $"Mes: {informacaoAnual.Meses[i]},  Despesa: {despesas[i]}, Receita: {receitas[i]}, Saldo Mensal: {saldos[i]}\n";
                }

                var response = await _openAIService.GetChatCompletionAsync(informacaoAnualString);
                return Ok(new { response });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
}

}