import { HTTPClient } from "./client";


export interface ICategoriaCriar {
    nome: string;
    tipoTransacao: number;
}

export interface ICategoriaAtualizar {
    id: number;
    nome: string;
    tipoTransacao: number;
}
export interface ICategoriaResponse {
    id: number;
    nome: string;
    tipoTransacao: string;
}



export const categoriaAPI = {
    async criarCategoria(categoria: ICategoriaCriar): Promise<{id: number}> {
        const response = await HTTPClient.post<ICategoriaResponse>('/api/Categoria/Criar', categoria);
        return response.data;
    },

    async listarCategorias(): Promise<ICategoriaResponse[]> {
        const response = await HTTPClient.get<ICategoriaResponse[]>('/api/Categoria/Listar');
        return response.data;
    },

    async atualizarCategoria(categoria: ICategoriaAtualizar): Promise<void> {
        await HTTPClient.put(`/api/Categoria/Atualizar`, categoria);
    },

    async deletarCategoria(id: number): Promise<void> {
        await HTTPClient.delete(`/api/Categoria/Deletar/${id}`);
    }

}