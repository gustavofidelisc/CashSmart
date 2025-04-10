import React, { useEffect, useState } from "react";
import style from "./Usuario.module.css";
import { IUsuario, usuarioAPI } from "../../services/usuarioAPI";
import { Form } from "react-bootstrap";
import { Sidebar } from "../../components/Sidebar/Sidebar";

export const UsuarioConfiguracao: React.FC = () => {
    const [usuario, setUsuario] = useState<IUsuario>();
    const [usuarioOriginal, setUsuarioOriginal] = useState<IUsuario>();
    const [formAlterado, setFormAlterado] = useState(false);

    const obterUsuario = async () => {
        try {
            const response = await usuarioAPI.obterUsuario();
            const dadosUsuario = {
                nome: response.nome,
                email: response.email
            };
            setUsuario(dadosUsuario);
            setUsuarioOriginal(dadosUsuario);
        } catch (error) {
            console.error("Erro ao obter usuário:", error);
        }
    };

    useEffect(() => {
        obterUsuario();
    }, []);

    const atualizarUsuario = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            if (!usuario) {
                alert("Nenhum usuário encontrado.");
                return;
            } 
            await usuarioAPI.atualizarUsuario(usuario);
            alert("Usuário atualizado com sucesso!");
            setUsuarioOriginal(usuario); // Atualiza o original após salvar
            setFormAlterado(false); // Desativa o botão novamente
        } catch (error) {
            console.error("Erro ao atualizar usuário:", error);
            alert("Erro ao atualizar usuário.");
        }
    };

    const usuarioAlterado = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setUsuario((prevState) => {
            const novoUsuario = { ...prevState!, [name]: value };
            
            const alterado = 
                novoUsuario.nome !== usuarioOriginal?.nome ||
                novoUsuario.email !== usuarioOriginal?.email;

            setFormAlterado(alterado);
            return novoUsuario;
        });
    };

    return (
        <Sidebar>
            <div className={style.container}>
                <h1 className={style.title}>Configurações do Usuário</h1>
                <div className={style.form_container}>
                    <Form className={style.form}>
                        <Form.Group className="mb-3" controlId="formNome">
                            <Form.Label>Nome</Form.Label>
                            <Form.Control 
                                type="text" 
                                name="nome"
                                placeholder="Digite seu nome" 
                                value={usuario?.nome || ""}
                                onChange={usuarioAlterado}
                                />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formEmail">
                            <Form.Label>Email</Form.Label>
                            <Form.Control 
                                type="email" 
                                name="email"
                                placeholder="Digite seu email" 
                                value={usuario?.email || ""}
                                onChange={usuarioAlterado}
                                />
                        </Form.Group>

                        <button 
                            className={style.button}  
                            onClick={atualizarUsuario}
                            disabled={!formAlterado} // Desativa se nada mudou
                            >
                            Atualizar
                        </button>
                    </Form>
                </div>
            </div>
        </Sidebar>
    );
};
