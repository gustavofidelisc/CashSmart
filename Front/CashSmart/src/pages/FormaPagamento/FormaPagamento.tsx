import { useEffect, useState } from "react";
import { Sidebar } from "../../components/Sidebar/Sidebar";
import style from "./FormaPagamento.module.css";
import { Button, Form, Modal, Table } from "react-bootstrap";
import {
    FormaPagamentoAPI,
    IFormaPagamentoAtualizar,
    IFormaPagamentoCriar,
    IFormaPagamentoResponse,
} from "../../services/formaPagamentoAPI";
import { AiFillDelete } from "react-icons/ai";
import { GoPencil } from "react-icons/go";

export const FormaPagamento: React.FC = () => {
  const [nome, setNome] = useState<string>("");
  const [formaPagamentoAtualizar, setFormaPagamentoAtualizar] = useState<IFormaPagamentoAtualizar | null>(null);
  const [formaPagamentoDeletar, setFormaPagamentoDeletar] = useState<IFormaPagamentoResponse | null>(null);

  const [showModalCriar, setShowModalCriar] = useState(false);
  const [showModalAtualizar, setShowModalAtualizar] = useState(false);
  const [showModalDeletar, setShowModalDeletar] = useState(false);

  const [formasPagamento, setFormasPagamento] = useState<IFormaPagamentoResponse[]>([]);

  const listarFormasPagamento = async () => {
    try {
      const response = await FormaPagamentoAPI.listarFormasPagamento();
      setFormasPagamento(response);
    } catch (error) {
      console.error("Erro ao listar formas de pagamento:", error);
    }
  };

  const cadastrarFormaPagamento = async () => {
    if (!nome) {
      alert("Preencha o nome da forma de pagamento!");
      return;
    }

    const data: IFormaPagamentoCriar = { nome };

    try {
      await FormaPagamentoAPI.criarFormaPagamento(data);
      setNome("");
      listarFormasPagamento();
      setShowModalCriar(false);
    } catch (error) {
      console.error("Erro ao cadastrar forma de pagamento:", error);
      alert("Erro ao cadastrar forma de pagamento.");
    }
  };

  const mostrarModalAtualizar = (forma: IFormaPagamentoResponse) => {
    setFormaPagamentoAtualizar({ id: forma.id, nome: forma.nome });
    setShowModalAtualizar(true);
  };

  const mostrarModalDeletar = (forma: IFormaPagamentoResponse) => {
    setFormaPagamentoDeletar(forma);
    setShowModalDeletar(true);
  };

  const fecharModalAtualizar = () => {
    setFormaPagamentoAtualizar(null);
    setShowModalAtualizar(false);
  };

  const fecharModalDeletar = () => {
    setFormaPagamentoDeletar(null);
    setShowModalDeletar(false);
  };

  const atualizarFormaPagamento = async () => {
    if (!formaPagamentoAtualizar || !formaPagamentoAtualizar.nome) {
      alert("Preencha todos os campos!");
      return;
    }

    try {
      await FormaPagamentoAPI.atualizarFormaPagamento(formaPagamentoAtualizar);
      listarFormasPagamento();
      setShowModalAtualizar(false);
    } catch (error) {
      console.error("Erro ao atualizar forma de pagamento:", error);
    }
  };

  const deletarFormaPagamento = async () => {
    if (!formaPagamentoDeletar) return;

    try {
      await FormaPagamentoAPI.deletarFormaPagamento(formaPagamentoDeletar.id);
      listarFormasPagamento();
      setShowModalDeletar(false);
    } catch (error) {
      console.error("Erro ao deletar forma de pagamento:", error);
      alert("Erro ao deletar forma de pagamento.");
    }
  };

  useEffect(() => {
    listarFormasPagamento();
  }, []);

  return (
    <Sidebar>
      <div className={style.Header}>
        <h2>Formas de Pagamento</h2>
        <button onClick={() => setShowModalCriar(true)}>+ Forma de Pagamento</button>
      </div>

      <Table responsive className={style.tabela}>
        <thead className={style.thead}>
          <tr>
            <th>Nome</th>
            <th></th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody className={style.tbody}>
          {formasPagamento.map((item) => (
            <tr key={item.id}>
              <td>{item.nome}</td>
              <td></td>
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
          <Modal.Title className="fs-1">Adicionar Forma de Pagamento</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Nome da Forma de Pagamento</Form.Label>
              <Form.Control
                type="text"
                placeholder="Ex: Cartão de Crédito, Boleto"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
                required
                className="fs-3"
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={cadastrarFormaPagamento} className="fs-4">Adicionar</Button>
          <Button variant="danger" onClick={() => setShowModalCriar(false)} className="fs-4">Fechar</Button>
        </Modal.Footer>
      </Modal>

      {/* Modal de Atualização */}
      <Modal show={showModalAtualizar} onHide={fecharModalAtualizar} size="lg" centered>
        <Modal.Header closeButton>
          <Modal.Title className="fs-1">Editar Forma de Pagamento</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Nome da Forma de Pagamento</Form.Label>
              <Form.Control
                type="text"
                placeholder="Ex: Cartão de Crédito, Boleto"
                value={formaPagamentoAtualizar?.nome || ""}
                onChange={(e) => setFormaPagamentoAtualizar({ ...formaPagamentoAtualizar!, nome: e.target.value })}
                required
                className="fs-3"
              />
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={atualizarFormaPagamento} className="fs-4">Atualizar</Button>
          <Button variant="danger" onClick={fecharModalAtualizar} className="fs-4">Fechar</Button>
        </Modal.Footer>
      </Modal>

      {/* Modal de Confirmação de Exclusão */}
      <Modal show={showModalDeletar} onHide={fecharModalDeletar} centered>
        <Modal.Header closeButton>
          <Modal.Title className="text-center w-100">Confirmar Exclusão</Modal.Title>
        </Modal.Header>
        <Modal.Body className="text-center">
          <p className="fs-4">Tem certeza que deseja excluir esta forma de pagamento?  <br />
          (Essa ação excluíra todas as transações associadas )</p>
          <p className="fw-bold">{formaPagamentoDeletar?.nome}</p>
        </Modal.Body>
        <Modal.Footer className="justify-content-center">
          <Button variant="secondary" onClick={fecharModalDeletar} className="fs-4">
            Cancelar
          </Button>
          <Button variant="danger" onClick={deletarFormaPagamento} className="fs-4">
            Confirmar Exclusão
          </Button>
        </Modal.Footer>
      </Modal>
    </Sidebar>
  );
};