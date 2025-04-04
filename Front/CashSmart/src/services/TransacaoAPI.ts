import dayjs, { Dayjs } from "dayjs";
import { HTTPClient } from "./client";

export interface ITransacaoCriar {
    descricao: string;
    valor: number;
    dataTransacao: string;
    categoriaId: number;
    formaPagamentoId: number;
}

export interface ITransacaoAtualizar {
    id: number;
    descricao: string;
    valor: number;
    dataTransacao: string;
    categoriaId: number;
    formaPagamentoId: number;
}

export interface ITransacaoResposta {
    id: number;
    descricao: string;
    valor: number;
    data: string;
    tipoTransacao: string;
    nomeCategoria: string;
    nomeFormaPagamento: string;
}

export interface ISaldoUsuario {
    saldo: number;
}

export interface ITransacaoInformacoesPorData {
    receitas: number,
    despesas: number,
}

export interface ItransacaoInformacoesGrafico {
    categoriaNomes: string[];
    valores: number[];
    tipoTransacao: number[];
}

export const transacaoAPI = {
    async listarTransacoes(): Promise<ITransacaoResposta[]> {
        const response = await HTTPClient.get<ITransacaoResposta[]>('/api/Transacao/Listar');
        return response.data;
    },

    async listarTransacoesPorData(dataInicial: Dayjs): Promise<ITransacaoResposta[]> {
        const dataFinal = dataInicial.endOf('month');
        const response = await HTTPClient.get<ITransacaoResposta[]>('/api/Transacao/Listar' + 
            `?dataInicial=${dataInicial.format('YYYY-MM-DDTHH:mm:ss[Z]')}` +
            `&dataFinal=${dataFinal.format('YYYY-MM-DDTHH:mm:ss[Z]')}`
        );
        return response.data;
    },
    async criarTransacao(transacao: ITransacaoCriar): Promise<{id: number}> {
        const response = await HTTPClient.post<ITransacaoResposta>('/api/Transacao/Criar', transacao);
        return response.data;
    },
    async atualizarTransacao(transacao: ITransacaoAtualizar): Promise<void> {
        await HTTPClient.put(`/api/Transacao/Atualizar`, transacao);
    },
    async buscarInformacoesTransacoesPorData(datainicial: Dayjs): Promise<ITransacaoInformacoesPorData> {
        const dataFinal = datainicial.endOf('month');
        
        // Usando UTC para ambas as datas
        const response = await HTTPClient.get<ITransacaoInformacoesPorData>(
            `/api/Transacao/InformacoesTransacoesPorData?` +
            `dataInicial=${datainicial.format('YYYY-MM-DDTHH:mm:ss[Z]')}` +
            `&dataFinal=${dataFinal.format('YYYY-MM-DDTHH:mm:ss[Z]')}`
        );
        return response.data;
    },

    async obterSaldoUsuario(dataAtual: Dayjs): Promise<ISaldoUsuario> {
        const dataFinal = dataAtual.endOf('month');

        const response = await HTTPClient.get<ISaldoUsuario>(`/api/Transacao/SaldoUsuario?` +
            `dataFinal=${dataFinal.format('YYYY-MM-DDTHH:mm:ss[Z]')}` 
        );
        return response.data;
    },

    async obterInformacoesGraficoPorData(dataInicial: Dayjs, tipoTransacao:number): Promise<ItransacaoInformacoesGrafico> {
        const dataFinal = dataInicial.endOf('month');

        
        const response = await HTTPClient.get<ItransacaoInformacoesGrafico>(
            `/api/Transacao/obterInformacoesGraficoPelaCategoria?` +
            `dataInicial=${dataInicial.format('YYYY-MM-DDTHH:mm:ss[Z]')}` +
            `&dataFinal=${dataFinal.format('YYYY-MM-DDTHH:mm:ss[Z]')}`+
            `&tipoTransacaoId=${tipoTransacao}`
        );
        return response.data;
    }
}