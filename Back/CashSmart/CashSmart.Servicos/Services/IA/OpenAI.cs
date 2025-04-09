using System.ClientModel;
using System.ClientModel.Primitives;
using CashSmart.Dominio.Shared;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;

namespace CashSmart.Servicos.Services.IA
{
    public class OpenAIService
    {
        private readonly GitHubTokenConfiguracoes _gitHubTokenConfiguracoes;
        private ChatClient _chatClient;
        private readonly List<ChatMessage> _messages;

        public OpenAIService(IOptions<GitHubTokenConfiguracoes> gitHubTokenConfiguracoes)
        {
            _gitHubTokenConfiguracoes = gitHubTokenConfiguracoes.Value;
            
            // Inicializa a lista de mensagens com a mensagem do sistema
            _messages = new List<ChatMessage>
            {
                new SystemChatMessage("You are a helpful financial assistant.\n\n" +
                "Com base no histórico completo de transações financeiras de um usuário, analise os dados e identifique padrões de gastos excessivos, oportunidades de economia e sugestões de organização financeira. ignore os dados que vierem como 0 no saldo mensal, despesas e receitas ao mesmo tempo\n\n" +
                "Gere um relatório com as seguintes instruções:\n\n" +
                "1. O tom deve ser amigável e objetivo, focado em melhorar a saúde financeira do usuário.\n" +
                "2. Para cada recomendação, siga este formato:\n" +
                "   - 100 palavras no maximo\n" +
                "   - informacoes como a pessoa pode utilizar melhor seu dinheiro\n" +
                "   - economiaPotencial: valor estimado de economia mensal, em reais (ex: 150.00)\n\n" +
                "4. Baseie-se em comparações com médias mensais, repetições de transações, categorias com alta concentração de gastos e falta de planejamento.\n" +
                "5. Evite recomendar coisas genéricas – personalize com base nos dados fornecidos.\n\n" +
                "A seguir estão os dados históricos do usuário (formato JSON/CSV/estruturado):\n")
            };
        }

        public void InitializeClient()
        {
            var openAIOptions = new OpenAIClientOptions()
            {
                Endpoint = new Uri(_gitHubTokenConfiguracoes.URI) // Ou seu endpoint específico
            };

            _chatClient = new ChatClient(
                _gitHubTokenConfiguracoes.Model,
                new ApiKeyCredential(_gitHubTokenConfiguracoes.GITHUB_TOKEN),
                openAIOptions);
        }

        public async Task<string> GetChatCompletionAsync(string userMessage)
        {
            if (_chatClient == null)
            {
                throw new InvalidOperationException("Client not initialized. Call InitializeClient first.");
            }

            // Adiciona a mensagem do usuário
            _messages.Add(new UserChatMessage(userMessage));

            var requestOptions = new ChatCompletionOptions()
            {
                Temperature = 0.7f, 
                TopP = 0.9f,
                MaxOutputTokenCount = 2000, 
            };

            try
            {
                var response = await _chatClient.CompleteChatAsync(_messages, requestOptions);
                
                if (response?.Value?.Content?.Count > 0)
                {
                    var assistantResponse = response.Value.Content[0].Text;
                    
                    return assistantResponse;
                }

                return "No response from AI.";
            }
            catch (ClientResultException ex)
            {
                // Log do erro (implemente seu sistema de logging)
                Console.WriteLine($"Error calling OpenAI: {ex.Message}");
                throw;
            }
        }

        
    }
}