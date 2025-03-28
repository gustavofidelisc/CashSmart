import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom"
import { Login } from "../pages/Auth/Login/Login"
import { Register } from "../pages/Auth/Register/Register"
import { AutenticacaoProvider } from "../Context/AutenticacaoContexto"


export const Rotas = () => {
    return(
        <AutenticacaoProvider>
            <Login>
                <BrowserRouter>
                    <Routes>
                        <Route path="/Cadastro" element={<Register/>}/>
                        <Route path="/home" element={<h1>olá</h1>}/>
                        <Route path="*" element={<Navigate to="/home" replace />} />
                    </Routes>
                </BrowserRouter>
            </Login>
        </AutenticacaoProvider>
    )
}