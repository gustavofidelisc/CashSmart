import { HTTPClient } from "./client";



interface IUsuarioData {
    email: string;
    senha: string;
    token: string;
}

interface IUsuarioRegistrar {
    id: string;
}

interface IAutenticacao {
    token: string;
}


export const AutenticacaoAPI = {
    async Login(email: string, senha: string) : Promise<IAutenticacao | Error> {
        try {
            const response = await HTTPClient.post<IUsuarioData>('/api/Autenticacao/Login', {
                email: email,
                senha: senha
            });
            return response.data
        } catch (error) {
            window.history.pushState(null, '', '/login');
            throw error; 
        }
    },

    async Registrar(nome: string, email: string, senha: string, confirmacaoSenha: string) : Promise<IUsuarioRegistrar | Error> {
        try {
            const response = await HTTPClient.post<IUsuarioRegistrar>('/api/Usuario/Criar', {
                nome: nome,
                email: email,
                senha: senha,
                confirmacaoSenha: confirmacaoSenha
            });
            return response.data
        } catch (error) {
            console.error("Erro ao registrar:", error);
            throw error; 
        }
    }
};