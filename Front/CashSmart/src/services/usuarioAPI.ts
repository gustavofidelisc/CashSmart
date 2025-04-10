import { HTTPClient } from "./client";

export interface IUsuarioAtualizar {
    nome: string;
    email: string;
}
export interface IUsuario{
    nome: string;
    email: string;
}

export const usuarioAPI = {
    async obterUsuario() : Promise<IUsuarioAtualizar> {
        try {
            const response = await HTTPClient.get<IUsuarioAtualizar>("/api/Usuario");
            return response.data;
        } catch (error) {
            console.error("Erro ao obter usuário:", error);
            throw error;
        }
    },
    async atualizarUsuario(usuario: IUsuarioAtualizar) : Promise<void> {
        try {
            await HTTPClient.put("/api/Usuario/Atualizar", usuario);
        } catch (error) {
            console.error("Erro ao atualizar usuário:", error);
            throw error;
        }
    }
}