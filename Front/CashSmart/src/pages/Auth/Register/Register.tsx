import style from './Register.module.css';
import background from '../AuthBackground.module.css';
import { MdEmail, MdLock } from 'react-icons/md';
import { IoPersonCircleOutline } from 'react-icons/io5';

import { Button, Form, InputGroup } from 'react-bootstrap';

import { useEffect, useState } from 'react';
import { AcessHeader } from '../../../components/AcessHeader/AcessHeader';
import { AutenticacaoAPI } from '../../../services/autenticacaoAPI';
import { useAutenticacaoContexto } from '../../../Context/AutenticacaoContexto';
import { Navigate, useNavigate } from 'react-router-dom';



export const Register: React.FC = () => {
    const [nome, setNome] = useState('');
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [confirmeSenha, setConfirmeSenha] = useState('');

    async function Registrar(){
        try {
            const response = await AutenticacaoAPI.Registrar(nome, email, senha, confirmeSenha);
        } catch (error) {
            console.error("Erro ao cadastrar:", error);
        }
    }

    return (
        <div className={background.container}>
            <AcessHeader navigateTo='/login' textButton="Entrar" />
            <main className={style.container_register}>
                <div>
                    <h2>
                        Cadastre-se no{' '}
                        <span className={style.color_green}>Cash</span>
                        <span className={style.color_blue}>Smart</span>
                    </h2>

                    <Form className={style.form} onSubmit={(e) => {
                        e.preventDefault();
                        Registrar();
                        }}>
                        <Form.Group className="mb-3" controlId="formNome">
                            <InputGroup size="lg" className={style.inputGroup} >
                                <InputGroup.Text id="nome">
                                    <IoPersonCircleOutline style={{ fontSize: '1.8rem' }} />
                                </InputGroup.Text>
                                <Form.Control
                                    className="fs-3 py-2"

                                    type="Nome"
                                    placeholder="Nome"
                                    size="lg"
                                    value={nome}
                                    onChange={(e) =>
                                        setNome(e.target.value)
                                    }
                                    required
                                />
                            </InputGroup>
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formEmail">
                            <InputGroup size="lg" className={style.inputGroup}>
                                <InputGroup.Text id="email">
                                    <MdEmail style={{ fontSize: '1.8rem' }}/>
                                </InputGroup.Text>
                                <Form.Control
                                    type="email"
                                    placeholder="Email"
                                    size="lg"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                    className="fs-3 py-2"
                                />
                            </InputGroup>
                        </Form.Group>
                        
                        <Form.Group controlId="formSenha" className="mb-3">
                            <InputGroup size="lg" className={style.inputGroup}>
                                <InputGroup.Text id="senha">
                                    <MdLock style={{ fontSize: '1.8rem' }} />
                                </InputGroup.Text>
                                <Form.Control
                                    className="fs-3 py-2"
                                    type="password"
                                    placeholder="Senha"
                                    size="lg"
                                    value={senha}
                                    onChange={(e) =>
                                        setSenha(e.target.value)
                                    }
                                    required
                                />
                            </InputGroup>
                        </Form.Group>

                        <Form.Group
                            controlId="formConfirmeSenha"
                            className="mb-3">
                            <InputGroup size="lg" className={style.inputGroup}>
                                <InputGroup.Text id="confirmeSenha">
                                    <MdLock style={{ fontSize: '1.8rem' }}/>
                                </InputGroup.Text>
                                <Form.Control
                                    className="fs-3 py-2"
                                    type="password"
                                    placeholder="Confirme a senha"
                                    size="lg"
                                    value={confirmeSenha}
                                    onChange={(e) =>
                                        setConfirmeSenha(e.target.value)
                                    }
                                    required
                                />
                            </InputGroup>
                        </Form.Group>
                        <Button
                            className={style.button_form}
                            size="lg"
                            variant="primary"
                            type="submit">
                            Cadastrar
                        </Button>
                    </Form>
                </div>
            </main>
        </div>
    );
};
