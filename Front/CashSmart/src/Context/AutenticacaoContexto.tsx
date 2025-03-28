import React, { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";
import { AutenticacaoAPI } from "../services/autenticacaoAPI";

interface IAutenticacaoData {
    estaAutenticado: boolean;
    login: (email: string, senha: string) => Promise<string | void>;
    logout: () => void;
}

interface IAutenticacaoProviderProps {
    children: React.ReactNode;
}

const AutenticacaoContexto = createContext({} as IAutenticacaoData);

export const AutenticacaoProvider: React.FC<IAutenticacaoProviderProps> = ({children}) => {
    const [token, setToken] = useState<string>();

    useEffect(() => {
        const tokenLocalStorage = localStorage.getItem('token');
        if (tokenLocalStorage) {
            setToken(JSON.parse(tokenLocalStorage));
        } else {
            setToken(undefined);
        }
    }, []);
    
    const handleLogin = useCallback( async(email: string, senha: string) => {
        const response = await AutenticacaoAPI.Login(email, senha);
        if (response instanceof Error) {
            return response.message;
        }else{
            localStorage.setItem('token', JSON.stringify(response.token));
            setToken(response.token);
        }
    }, []);

    const handleLogout = useCallback( () => {
        localStorage.removeItem('token');
        setToken(undefined);
    }, []);


    const estaAutenticado = useMemo(() => token !== undefined, [token]); 
    return (
        <AutenticacaoContexto.Provider value={{ estaAutenticado,login: handleLogin, logout: handleLogout }}>
            {children}
        </AutenticacaoContexto.Provider>
    );
}

export const useAutenticacaoContexto = () => useContext(AutenticacaoContexto);