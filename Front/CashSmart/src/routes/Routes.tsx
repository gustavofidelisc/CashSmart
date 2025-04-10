import { BrowserRouter, Navigate, Route, Routes, Outlet } from "react-router-dom";
import { Login } from "../pages/Auth/Login/Login";
import { Register } from "../pages/Auth/Register/Register";
import { AutenticacaoProvider, useAutenticacaoContexto } from "../Context/AutenticacaoContexto";
import { Dashboard } from "../pages/Dashboard/Dashboard";
import { Categoria } from "../pages/Categoria/Categoria";
import { FormaPagamento } from "../pages/FormaPagamento/FormaPagamento";
import { SidebarProvider } from "../Context/SidebarContexto";
import { Relatorio } from "../pages/Relatorio/Relatorio";
import { UsuarioConfiguracao } from "../pages/Usuario/Usuario";

// Layout para rotas públicas (visível apenas para usuários NÃO autenticados)
const RotasPublicas: React.FC = () => {
    const { estaAutenticado } = useAutenticacaoContexto();
    return estaAutenticado ? <Navigate to="/home" replace /> : <Outlet />;
};

// Layout para rotas privadas (acessível apenas para usuários autenticados)
const RotasPrivadas: React.FC = () => {
    const { estaAutenticado } = useAutenticacaoContexto();
    return estaAutenticado ? <Outlet /> : <Navigate to="/login" replace />;
};

export const Rotas: React.FC = () => {
    return (
        <AutenticacaoProvider>
            <SidebarProvider>
            <BrowserRouter>
                <Routes>
                    {/* Rotas públicas */}
                    <Route element={<RotasPublicas />}>
                        <Route path="/login" element={<Login />} />
                        <Route path="/cadastro" element={<Register />} />
                    </Route>

                    {/* Rotas privadas */}
                    <Route element={<RotasPrivadas />}>
                        <Route path="/Dashboard" element={<Dashboard/>} />
                        <Route path="/Relatorios" element={<Relatorio/>} />
                        <Route path="/Categorias" element={<Categoria/>} />
                        <Route path="/Metodos_pagamentos" element={<FormaPagamento/>} />
                        <Route path="/Usuario" element={<UsuarioConfiguracao />} />
                    </Route>
                    

                    {/* Redirecionamento de rotas desconhecidas */}
                    <Route path="*" element={<Navigate to="/Dashboard" replace />} />
                </Routes>
            </BrowserRouter>
            </SidebarProvider>
        </AutenticacaoProvider>
    );
};
