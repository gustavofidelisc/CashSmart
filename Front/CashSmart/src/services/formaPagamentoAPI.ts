import { HTTPClient } from "./client";


export interface IFormaPagamentoCriar {
    nome: string;
}

export interface IFormaPagamentoAtualizar {
    id: number;
    nome: string;
}
export interface IFormaPagamentoResponse {
    id: number;
    nome: string;
}



export const FormaPagamentoAPI = {
    async criarFormaPagamento(FormaPagamento: IFormaPagamentoCriar): Promise<{id: number}> {
        const response = await HTTPClient.post<IFormaPagamentoResponse>('/api/FormaPagamento/Criar', FormaPagamento);
        return response.data;
    },

    async listarFormasPagamento(): Promise<IFormaPagamentoResponse[]> {
        const response = await HTTPClient.get<IFormaPagamentoResponse[]>('/api/FormaPagamento/Listar');
        return response.data;
    },

    async atualizarFormaPagamento(FormaPagamento: IFormaPagamentoAtualizar): Promise<void> {
        await HTTPClient.put(`/api/FormaPagamento/Atualizar`, FormaPagamento);
    },

    async deletarFormaPagamento(id: number): Promise<void> {
        await HTTPClient.delete(`/api/FormaPagamento/Deletar/${id}`);
    }

}