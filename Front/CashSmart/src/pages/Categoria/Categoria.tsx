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

export const Categoria: React.FC = () => {
  const [nome, setNome] = useState<string>("");
  const [tipo, setTipo] = useState<number>(0);
  const [categoriaAtualizar, setCategoriaAtualizar] = useState<ICategoriaAtualizar | null>(null);

  const [showModalCriar, setShowModalCriar] = useState(false);
  const [showModalAtualizar, setShowModalAtualizar] = useState(false);

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

  const fecharModalAtualizar = () => {
    setCategoriaAtualizar(null);
    setShowModalAtualizar(false);
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
              <td>
                <Button variant="warning" size="sm" className={style.button} onClick={() => mostrarModalAtualizar(item)}>
                  Editar
                </Button>
                <Button variant="danger" size="sm">Excluir</Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>

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
    </SideBar>
  );
};
