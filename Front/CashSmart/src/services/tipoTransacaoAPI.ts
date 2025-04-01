import { HTTPClient } from "./client";

export interface ITipoTransacaoResposta {
    id: number;
    nome: string;
}

export const tipoTransacaoAPI = {
    async listarTipoTransacao(): Promise<ITipoTransacaoResposta[]> {
        const response = await HTTPClient.get<ITipoTransacaoResposta[]>('/api/TipoTransacao/Listar');
        return response.data;
    },
};