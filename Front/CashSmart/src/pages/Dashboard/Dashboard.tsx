import { useEffect, useState } from "react";
import { SideBar } from "../../components/Sidebar/Sidebar";
import style from "./Dashboard.module.css";
import { Button, Card, Form, Modal, Table } from "react-bootstrap";
import { categoriaAPI, ICategoriaResponse } from "../../services/categoriaAPI";
import { FormaPagamentoAPI, IFormaPagamentoResponse } from "../../services/formaPagamentoAPI";
import { transacaoAPI, ITransacaoCriar, ITransacaoAtualizar, ITransacaoResposta } from "../../services/TransacaoAPI";
import { CardSaldo } from "../../components/Card/CardSaldo";

import { FiChevronLeft, FiChevronRight } from "react-icons/fi"; // Ícones (opcional)
import { FaArrowTrendDown } from "react-icons/fa6";
import { FaArrowTrendUp } from "react-icons/fa6";
import { MdAttachMoney } from "react-icons/md";

import { IoWalletOutline } from "react-icons/io5";


export const Dashboard: React.FC = () => {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<number>(0);
  const [dataTransacao, setDataTransacao] = useState("");
  const [categoriaId, setCategoriaId] = useState<number>(0);
  const [formaPagamentoId, setFormaPagamentoId] = useState<number>(0);


  const [showModalCriar, setShowModalCriar] = useState(false);
  const [showModalAtualizar, setShowModalAtualizar] = useState(false);

  const [transacoes, setTransacoes] = useState<ITransacaoResposta[]>([]);
  const [categorias, setCategorias] = useState<ICategoriaResponse[]>([]);
  const [formasPagamento, setFormasPagamento] = useState<IFormaPagamentoResponse[]>([]);

  const [dataAtual, setDataAtual] = useState(new Date());

  // Nomes dos meses em português
  const meses = [
    "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
    "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
  ];

  // Avançar mês
  const proximoMes = () => {
    const newDate = new Date(dataAtual);
    newDate.setMonth(newDate.getMonth() + 1);
    setDataAtual(newDate);
  };

  // Voltar mês
  const mesAnterior = () => {
    const newDate = new Date(dataAtual);
    newDate.setMonth(newDate.getMonth() - 1);
    setDataAtual(newDate);
  };

  // Formatar ano e mês
  const currentMonth = meses[dataAtual.getMonth()];
  const currentYear = dataAtual.getFullYear();

  const listarTransacoes = async () => {
    try {
      const response = await transacaoAPI.listarTransacoes();
      setTransacoes(response);
    } catch (error) {
      console.error("Erro ao listar transações:", error);
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

  const listarFormasPagamento = async () => {
    try {
      const response = await FormaPagamentoAPI.listarFormasPagamento();
      setFormasPagamento(response);
    } catch (error) {
      console.error("Erro ao listar formas de pagamento:", error);
    }
  };

  const cadastrarTransacao = async () => {
    if ( valor <= 0 || !dataTransacao || categoriaId === 0 || formaPagamentoId === 0) {
      alert("Preencha todos os campos corretamente!");
      return;
    }

    const data: ITransacaoCriar = { descricao, valor, dataTransacao, categoriaId, formaPagamentoId };

    try {
      await transacaoAPI.criarTransacao(data);
      setDescricao("");
      setValor(0);
      setDataTransacao("");
      setCategoriaId(0);
      setFormaPagamentoId(0);
      listarTransacoes();
      setShowModalCriar(false);
    } catch (error) {
      console.error("Erro ao cadastrar transação:", error);
      alert("Erro ao cadastrar transação.");
    }
  };

  useEffect(() => {
    listarTransacoes();
    listarCategorias();
    listarFormasPagamento();
  }, []);

  return (
    <SideBar>
      <div className={style.Header}>
        <h2>Transações</h2>
        <button onClick={() => setShowModalCriar(true)}>+ Transação</button>
      </div>

      <Card >
        <Card.Body className={style.cardDate}>
          <FiChevronLeft onClick={mesAnterior} /> {/* Ícone de seta esquerda */}

          <div className="month-year-display">
            {currentMonth} de {currentYear}
          </div>

          <FiChevronRight onClick={proximoMes} /> {/* Ícone de seta direita */}
        </Card.Body>
    </Card>

      <div className={style.cards}>
        <CardSaldo icon={<IoWalletOutline/>} saldo={10000000000} title="Saldo Atual"/>
        <CardSaldo icon={<FaArrowTrendUp/>} saldo={100} title="Receita do Mês"/>
        <CardSaldo icon={<FaArrowTrendDown/>} saldo={100} title="Despesas do Mês"/>
        <CardSaldo icon={<MdAttachMoney/>} saldo={100} title=" Saldo Projetado"/>

      </div>


      <Table responsive className={style.tabela}>
        <thead className={style.thead}>
          <tr>
            <th>Descrição</th>
            <th>Valor</th>
            <th>Data</th>
            <th>Categoria</th>
            <th>Forma de Pagamento</th>
            <th>Tipo</th>
          </tr>
        </thead>
        <tbody className={style.tbody}>
          {transacoes.map((item) => (
            <tr key={item.id}>
              <td>{item.descricao}</td>
              <td>{item.valor}</td>
              <td>{item.data}</td>
              <td>{item.nomeCategoria}</td>
              <td>{item.nomeFormaPagamento}</td>
              <td>{item.tipoTransacao}</td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Modal show={showModalCriar} onHide={() => setShowModalCriar(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Adicionar Transação</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group>
              <Form.Label>Descrição</Form.Label>
              <Form.Control type="text" value={descricao} onChange={(e) => setDescricao(e.target.value)} />
            </Form.Group>
            <Form.Group>
              <Form.Label>Valor</Form.Label>
              <Form.Control type="number" value={valor} onChange={(e) => setValor(parseFloat(e.target.value))} />
            </Form.Group>
            <Form.Group>
              <Form.Label>Data</Form.Label>
              <Form.Control type="date" value={dataTransacao} onChange={(e) => setDataTransacao(e.target.value)} />
            </Form.Group>
            <Form.Group>
              <Form.Label>Categoria</Form.Label>
              <Form.Select value={categoriaId} onChange={(e) => setCategoriaId(Number(e.target.value))}>
                <option value={0}>Selecione</option>
                {categorias.map((item) => (
                  <option key={item.id} value={item.id}>{item.nome}</option>
                ))}
              </Form.Select>
            </Form.Group>
            <Form.Group>
              <Form.Label>Forma de Pagamento</Form.Label>
              <Form.Select value={formaPagamentoId} onChange={(e) => setFormaPagamentoId(Number(e.target.value))}>
                <option value={0}>Selecione</option>
                {formasPagamento.map((item) => (
                  <option key={item.id} value={item.id}>{item.nome}</option>
                ))}
              </Form.Select>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={cadastrarTransacao}>Adicionar</Button>
          <Button variant="danger" onClick={() => setShowModalCriar(false)}>Fechar</Button>
        </Modal.Footer>
      </Modal>
    </SideBar>
  );
};
