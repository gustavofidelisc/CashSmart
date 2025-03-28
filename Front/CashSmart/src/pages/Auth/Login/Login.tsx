import { Button, Form, InputGroup } from 'react-bootstrap';
import style from './Login.module.css';
import { use, useEffect, useState } from 'react';
import { AcessHeader } from '../../../components/AcessHeader/AcessHeader';
import background from '../AuthBackground.module.css';

import { MdEmail, MdLock } from 'react-icons/md';
import { useAutenticacaoContexto } from '../../../Context/AutenticacaoContexto';
import { Register } from '../Register/Register';

interface ILoginProps {
    children: React.ReactNode; 
}


export const Login: React.FC<ILoginProps> = ({children}) => {
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');

    const {estaAutenticado, login} = useAutenticacaoContexto();
    if (estaAutenticado) return  (
        <>{children}</>
    )

    // Permitir a tela de cadastro sem autenticação
    if (location.pathname === "/cadastro") {
        return <Register />;
    }

    
    return (
        <div className={background.container}>
            <AcessHeader navigateTo='/cadastro' textButton="Cadastrar-se" />

            <main className={style.container_login}>
                <div>
                    <h2>
                        Bem vindo ao{' '}
                        <span className={style.color_green}>Cash</span>
                        <span className={style.color_blue}>Smart</span>
                    </h2>
                    <p>Faça login para organizar suas financias</p>
                    <Form className={style.form} onSubmit={(e) => {
                        e.preventDefault();
                        login(email, senha)
                    }}>
                        <Form.Group className="mb-3" controlId="formEmail">
                            <InputGroup size="lg" className={style.inputGroup}>
                                <InputGroup.Text id={style.email}>
                                    <MdEmail style={{ fontSize: '1.8rem' }}/>
                                </InputGroup.Text>
                                <Form.Control

                                    type="email"
                                    size="lg"
                                    placeholder="Email"
                                    aria-describedby="email"
                                    
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                    className="fs-3 py-2"
                                />
                            </InputGroup>
                        </Form.Group>
                        <Form.Group
                            controlId="formSenha"
                            className="mb-3"
                            id={style.formSenha}>
                            <InputGroup size="lg" className={style.inputGroup}>
                                <InputGroup.Text id={style.senha}>
                                    <MdLock style={{ fontSize: '1.8rem' }} />
                                </InputGroup.Text>
                                <Form.Control
                                    className="fs-3 py-2"
                                    placeholder="Senha"
                                    type='password'
                                    name="senha"
                                    size="lg"
                                    value={senha}
                                    onChange={(e) => setSenha(e.target.value)}
                                    required
                                />
                            </InputGroup>
                        </Form.Group>
                        <Button
                            className={style.button}
                            variant="primary"
                            type="submit">
                            Entrar
                        </Button>
                    </Form>
                </div>
            </main>
        </div>
    );
};
