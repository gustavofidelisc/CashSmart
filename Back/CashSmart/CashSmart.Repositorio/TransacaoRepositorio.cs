using CashSmart.Dominio.Entidades;
using CashSmart.Repositorio.Contexto;
using CashSmart.Repositorio.Contratos;
using CashSmart.Repositorio.Models;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;

namespace CashSmart.Repositorio
{
    public class TransacaoRepositorio : BaseRepositorio, ITransacaoRepositorio
    {
        public TransacaoRepositorio(CashSmartContexto context) : base(context)
        {
        }

        public async Task<int> AdicionarTransacaoAsync(Transacao transacao){
            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();
            return transacao.Id;
        }

        public async Task<IEnumerable<Transacao>> obterTodasTransacoesUsuarioAsync(Guid usuarioId){
            return await _context.Transacoes.Include(t => t.Categoria)
                    .Include(t => t.FormaPagamento)
                    .Where(t => t.UsuarioId == usuarioId).ToListAsync();
        }

        public async Task<IEnumerable<Transacao>> obterTodasTransacoesUsuarioPorDataAsync(Guid usuarioId, DateTime dataInicial, DateTime dataFinal){
            return await _context.Transacoes.Include(t => t.Categoria)
                    .Include(t => t.FormaPagamento)
                    .OrderByDescending(t => t.Data)
                    .Where(t => t.UsuarioId == usuarioId && t.Data >= dataInicial && t.Data <= dataFinal).ToListAsync();
        }


        public async Task<Transacao> obterTransacaoPorUsuarioAsync(int id, Guid usuarioId){
            return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.FormaPagamento)
            .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuarioId);
        }

        public async Task AtualizarTransacaoAsync(Transacao transacao){
            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverTransacaoAsync(Transacao transacao){
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<TransacaoInformacoes> obterInformacoesTransacoesPorData(Guid usuarioId, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                var informacoes = await _context.Database.GetDbConnection()
                    .QuerySingleOrDefaultAsync<TransacaoInformacoesResposta>(
                        "SP_SALDOS_INFORMACOES_MES", 
                        new
                        {
                            ID_USUARIO = usuarioId,  // Nome correto do parâmetro
                            DATA_INICIAL = dataInicial,
                            DATA_FINAL = dataFinal   // Nome consistente
                        }, 
                        commandType: CommandType.StoredProcedure);

                if (informacoes != null)
                {
                    return new TransacaoInformacoes
                    {
                        Receitas = informacoes.TOTAL_RECEITA,
                        Despesas = informacoes.TOTAL_DESPESA
                    };
                }
                
                return null;
            }
            catch (Exception ex)
            {
                // Logar o erro
                throw new Exception("Erro ao obter informações de transações por data.", ex);
            }
        }

        public async Task<SaldoUsuario> obterSaldoUsuario(Guid usuarioId, DateTime dataFinal)
        {
            try
            {
                var informacoes = await _context.Database.GetDbConnection()
                    .QuerySingleOrDefaultAsync<SaldoUsuarioResposta>(
                        "SP_SALDO_ATUAL_USUARIO", 
                        new  
                        {
                            ID_USUARIO = usuarioId,  // Nome correto do parâmetro
                            DATA_ATUAL = dataFinal

                        }, 
                        commandType: CommandType.StoredProcedure);

                if (informacoes != null)
                {
                    return new SaldoUsuario
                    {
                        Saldo = informacoes.SALDO_ATUAL
                    };
                }
                
                return null;
            }
            catch (Exception ex)
            {
                // Logar o erro
                throw new Exception("Erro ao obter informações de transações por data." + ex.Message, ex);
            }
        }

        public async Task<GraficoInformacoes> obterInformacoesGraficoPelaCategoria (Guid usuarioId, DateTime dataInicial, DateTime dataFinal, int tipoTransacaoId)
        {
            try{
                var informacoes = await _context.Database.GetDbConnection()
            .QueryAsync<GraficoInformacoesResposta>(
                "SP_INFORMACOES_GRAFICO_MES", 
                new  
                {
                    ID_USUARIO = usuarioId,
                    DATA_INICIAL = dataInicial,
                    DATA_FINAL = dataFinal,
                    TIPO_TRANSACAO = tipoTransacaoId
                }, 
                commandType: CommandType.StoredProcedure);

            var listaInformacoes = informacoes.ToList();

            return new GraficoInformacoes
            {
                CategoriaNomes = listaInformacoes.Select(i => i.Nomes).ToArray(),
                Valores = listaInformacoes.Select(i => i.Valores).ToArray(),
                TipoTransacao = listaInformacoes.Select(i => i.TipoTransacao).ToArray()
            };
            }
            catch (Exception ex)
            {
                // Logar o erro
                throw new Exception("Erro ao obter informações de transações por data." +ex.Message, ex);
            }
        }
    }
}
