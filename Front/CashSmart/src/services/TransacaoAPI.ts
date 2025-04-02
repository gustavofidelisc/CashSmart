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

export const transacaoAPI = {
    async listarTransacoes(): Promise<ITransacaoResposta[]> {
        const response = await HTTPClient.get<ITransacaoResposta[]>('/api/Transacao/Listar');
        return response.data;
    },
    async criarTransacao(transacao: ITransacaoCriar): Promise<{id: number}> {
        const response = await HTTPClient.post<ITransacaoResposta>('/api/Transacao/Criar', transacao);
        return response.data;
    },
    async atualizarTransacao(transacao: ITransacaoAtualizar): Promise<void> {
        await HTTPClient.put(`/api/Transacao/Atualizar`, transacao);
    }
}