import { HTTPClient } from "./client";

export interface ITransacaoInformacoesIA {
    response: string;
    }

export const IAAPI = {
    async obterInformacoesIA(ano: number): Promise<ITransacaoInformacoesIA> {
        const response = await HTTPClient.post<ITransacaoInformacoesIA>(`/api/IA/Analise/IA?ano=${ano}`, {  });
        return response.data;
    },
};