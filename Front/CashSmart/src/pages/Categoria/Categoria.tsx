import { useEffect, useState } from "react";
import { SideBar } from "../../components/Sidebar/Sidebar";
import style from "./Categoria.module.css";
import { Button, Form, Modal, Table } from "react-bootstrap";
import {
  ITipoTransacaoResposta,
  tipoTransacaoAPI,
} from "../../services/tipoTransacaoAPI";
import {
  categoriaAPI,
  ICategoriaAtualizar,
  ICategoriaCriar,
  ICategoriaResponse,
} from "../../services/categoriaAPI";

import { GoPencil } from "react-icons/go";
import { AiFillDelete } from "react-icons/ai";

export const Categoria: React.FC = () => {
  const [nome, setNome] = useState<string>("");
  const [tipo, setTipo] = useState<number>(0);
  const [categoriaAtualizar, setCategoriaAtualizar] = useState<ICategoriaAtualizar | null>(null);
  const [categoriaDeletar, setCategoriaDeletar] = useState<ICategoriaResponse | null>(null);

  const [showModalCriar, setShowModalCriar] = useState(false);
  const [showModalAtualizar, setShowModalAtualizar] = useState(false);
  const [showModalDeletar, setShowModalDeletar] = useState(false);

  const [tipoTransacao, setTipoTransacao] = useState<ITipoTransacaoResposta[]>([]);
  const [categorias, setCategorias] = useState<ICategoriaResponse[]>([]);

  const listarTipoTransacao = async () => {
    try {
      const response = await tipoTransacaoAPI.listarTipoTransacao();
      setTipoTransacao(response);
    } catch (error) {
      console.error("Erro ao listar tipos de transação:", error);
    }
  };

  const listarCategorias = async () => {
    try {
      const response = await categoriaAPI.listarCategorias();
      setCategorias(response);
    } catch (error) {
      console.error("Erro ao listar categorias:", error);
    }
  };

  const cadastrarCategoria = async () => {
    if (!nome || tipo === 0) {
      alert("Preencha todos os campos!");
      return;
    }

    const data: ICategoriaCriar = { nome, tipoTransacao: tipo };

    try {
      await categoriaAPI.criarCategoria(data);
      setNome("");
      setTipo(0);
      listarCategorias();
      setShowModalCriar(false);
    } catch (error) {
      console.error("Erro ao cadastrar categoria:", error);
      alert("Erro ao cadastrar categoria.");
    }
  };

  const mostrarModalAtualizar = (categoria: ICategoriaResponse) => {
    setCategoriaAtualizar({
      id: categoria.id,
      nome: categoria.nome,
      tipoTransacao: categoria.tipoTransacao === "Receita" ? 1 : 2,
    });
    setShowModalAtualizar(true);
  };

  const mostrarModalDeletar = (categoria: ICategoriaResponse) => {
    setCategoriaDeletar(categoria);
    setShowModalDeletar(true);
  };

  const fecharModalAtualizar = () => {
    setCategoriaAtualizar(null);
    setShowModalAtualizar(false);
  };

  const fecharModalDeletar = () => {
    setCategoriaDeletar(null);
    setShowModalDeletar(false);
  };

  const atualizarCategoria = async () => {
    if (!categoriaAtualizar || !categoriaAtualizar.nome || categoriaAtualizar.tipoTransacao === 0) {
      alert("Preencha todos os campos!");
      return;
    }

    try {
      await categoriaAPI.atualizarCategoria(categoriaAtualizar);
      listarCategorias();
      setShowModalAtualizar(false);
    } catch (error) {
      console.error("Erro ao atualizar categoria:", error);
    }
  };

  const deletarCategoria = async () => {
    if (!categoriaDeletar) return;

    try {
      await categoriaAPI.deletarCategoria(categoriaDeletar.id);
      listarCategorias();
      setShowModalDeletar(false);
    } catch (error) {
      console.error("Erro ao deletar categoria:", error);
      alert("Erro ao deletar categoria.");
    }
  };

  useEffect(() => {
    listarTipoTransacao();
    listarCategorias();
  }, []);

  return (
    <SideBar>
      <div className={style.Header}>
        <h2>Categorias</h2>
        <button onClick={() => setShowModalCriar(true)}>+ Categoria</button>
      </div>

      <Table responsive className={style.tabela}>
        <thead className={style.thead}>
          <tr>
            <th>Nome</th>
            <th>Tipo de Categoria</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody className={style.tbody}>
          {categorias.map((item) => (
            <tr key={item.id} style={{ backgroundColor: item.tipoTransacao === "Receita" ? "#d4edda" : "#f8d7da" }}>
              <td>{item.nome}</td>
              <td>{item.tipoTransacao}</td>
              <td className={style.acoes}>
                <Button variant="warning" size="sm" className={style.button} onClick={() => mostrarModalAtualizar(item)}>
                  <GoPencil size={20} color="white" />
                </Button>
                <Button variant="danger" size="sm" onClick={() => mostrarModalDeletar(item)}>
                  <AiFillDelete size={20} color="white" />
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

      {/* Modal de Criação */}
      <Modal show={showModalCriar} onHide={() => setShowModalCriar(false)} size="lg" centered>
        <Modal.Header closeButton>
          <Modal.Title className="fs-1">Adicionar Categoria</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Nome da Categoria</Form.Label>
              <Form.Control
                type="text"
                placeholder="Ex: Alimentação, Transporte"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
                required
                className="fs-3"
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Tipo de Categoria</Form.Label>
              <Form.Select value={tipo} onChange={(e) => setTipo(parseInt(e.target.value))} required className="fs-3">
                <option value={0}>Selecione o tipo de categoria</option>
                {tipoTransacao.map((item) => (
                  <option key={item.id} value={item.id}>{item.nome}</option>
                ))}
              </Form.Select>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={cadastrarCategoria} className="fs-4">Adicionar</Button>
          <Button variant="danger" onClick={() => setShowModalCriar(false)} className="fs-4">Fechar</Button>
        </Modal.Footer>
      </Modal>

      {/* Modal de Atualização */}
      <Modal show={showModalAtualizar} onHide={fecharModalAtualizar} size="lg" centered>
        <Modal.Header closeButton>
          <Modal.Title className="fs-1">Editar Categoria</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Nome da Categoria</Form.Label>
              <Form.Control
                type="text"
                placeholder="Ex: Alimentação, Transporte"
                value={categoriaAtualizar?.nome || ""}
                onChange={(e) => setCategoriaAtualizar({ ...categoriaAtualizar!, nome: e.target.value })}
                required
                className="fs-3"
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Tipo de Categoria</Form.Label>
              <Form.Select
                value={categoriaAtualizar?.tipoTransacao || 0}
                onChange={(e) => setCategoriaAtualizar({ ...categoriaAtualizar!, tipoTransacao: parseInt(e.target.value) })}
                required
                className="fs-3"
              >
                <option value={0}>Selecione o tipo de categoria</option>
                {tipoTransacao.map((item) => (
                  <option key={item.id} value={item.id}>{item.nome}</option>
                ))}
              </Form.Select>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={atualizarCategoria} className="fs-4">Atualizar</Button>
          <Button variant="danger" onClick={fecharModalAtualizar} className="fs-4">Fechar</Button>
        </Modal.Footer>
      </Modal>

      {/* Modal de Confirmação de Exclusão */}
      <Modal show={showModalDeletar} onHide={fecharModalDeletar} centered>
        <Modal.Header closeButton>
          <Modal.Title className="text-center w-100">Confirmar Exclusão</Modal.Title>
        </Modal.Header>
        <Modal.Body className="text-center">
          <p className="fs-4">Tem certeza que deseja excluir esta categoria? <br />
            (Essa ação excluíra todas as transações associadas )</p>
          <p className="fw-bold">{categoriaDeletar?.nome}</p>
          <p style={{ color: categoriaDeletar?.tipoTransacao === "Receita" ? '#28a745' : '#dc3545' }} className="fs-5">
            {categoriaDeletar?.tipoTransacao}
          </p>
        </Modal.Body>
        <Modal.Footer className="justify-content-center">
          <Button variant="danger" onClick={deletarCategoria} className="fs-4">
            Confirmar Exclusão
          </Button>
          <Button variant="secondary" onClick={fecharModalDeletar} className="fs-4">
            Cancelar
          </Button>
        </Modal.Footer>
      </Modal>
    </SideBar>
  );
};