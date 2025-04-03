import { useEffect, useState } from "react";
import { SideBar } from "../../components/Sidebar/Sidebar";
import style from "./Dashboard.module.css";
import { Button, Card, Form, Modal, Table, ToggleButton, ToggleButtonGroup } from "react-bootstrap";
import { categoriaAPI, ICategoriaResponse } from "../../services/categoriaAPI";
import { FormaPagamentoAPI, IFormaPagamentoResponse } from "../../services/formaPagamentoAPI";
import { transacaoAPI, ITransacaoCriar, ITransacaoAtualizar, ITransacaoResposta } from "../../services/TransacaoAPI";
import { CardSaldo } from "../../components/Card/CardSaldo";

import { FiChevronLeft, FiChevronRight } from "react-icons/fi";
import { FaArrowTrendDown } from "react-icons/fa6";
import { FaArrowTrendUp } from "react-icons/fa6";
import { MdAttachMoney } from "react-icons/md";
import { IoWalletOutline } from "react-icons/io5";
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import { Grafico } from "../../components/Graficos/Grafico";

dayjs.extend(utc);

export const Dashboard: React.FC = () => {
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<number>(0);
  const [dataTransacao, setDataTransacao] = useState("");
  const [categoriaId, setCategoriaId] = useState<number>(0);
  const [formaPagamentoId, setFormaPagamentoId] = useState<number>(0);
  const [tipoTransacao, setTipoTransacao] = useState<'Receita' | 'Despesa'>('Despesa');

  const [despesas, setDespesas] = useState<number>(0);
  const [receitas, setReceitas] = useState<number>(0);

  const [showModalCriar, setShowModalCriar] = useState(false);
  const [showModalAtualizar, setShowModalAtualizar] = useState(false);

  const [transacoes, setTransacoes] = useState<ITransacaoResposta[]>([]);
  const [categorias, setCategorias] = useState<ICategoriaResponse[]>([]);
  const [categoriasFiltradas, setCategoriasFiltradas] = useState<ICategoriaResponse[]>([]);
  const [formasPagamento, setFormasPagamento] = useState<IFormaPagamentoResponse[]>([]);

  const [dataAtual, setDataAtual] = useState(dayjs().startOf('month'));
  const [saldoUsuario, setSaldoUsuario] = useState<number>(0);
  const [receitasMesesAFrente, setReceitasMesesAFrente] = useState<number>(0);

  const meses = [
    "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
    "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
  ];

  const proximoMes = () => {
    setDataAtual(dataAtual.add(1, 'month').startOf('month'));
  };

  const mesAnterior = () => {
    setDataAtual(dataAtual.subtract(1, 'month').startOf('month'));
  };

  const currentMonth = meses[dataAtual.month()];
  const currentYear = dataAtual.year();

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
      // Filtra inicialmente por Despesa (valor padrão)
      setCategoriasFiltradas(response.filter(c => c.tipoTransacao === 'Despesa'));
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

  const handleTipoTransacaoChange = (tipo: 'Receita' | 'Despesa') => {
    setTipoTransacao(tipo);
    setCategoriaId(0); // Reseta a categoria selecionada
    setCategoriasFiltradas(categorias.filter(c => c.tipoTransacao === tipo));
  };

  const cadastrarTransacao = async () => {
    if (valor <= 0 || !dataTransacao || categoriaId === 0 || formaPagamentoId === 0) {
      alert("Preencha todos os campos corretamente!");
      return;
    }

    const data: ITransacaoCriar = { 
      descricao, 
      valor,
      dataTransacao, 
      categoriaId, 
      formaPagamentoId 
    };

    try {
      await transacaoAPI.criarTransacao(data);
      setDescricao("");
      setValor(0);
      setDataTransacao("");
      setCategoriaId(0);
      setFormaPagamentoId(0);
      setTipoTransacao('Despesa');
      listarTransacoes();
      fetchTransacoesPorData();
      obterSaldoUsuario();
      setShowModalCriar(false);
    } catch (error) {
      console.error("Erro ao cadastrar transação:", error);
      alert("Erro ao cadastrar transação.");
    }
  };

  const obterSaldoUsuario = async () => {
    try {
      const response = await transacaoAPI.obterSaldoUsuario(dataAtual);
      setSaldoUsuario(response.saldo);
    } catch (error) {
      console.error("Erro ao obter saldo do usuário:", error);
    }
  };

  useEffect(() => {
    listarTransacoes();
    listarCategorias();
    listarFormasPagamento();
    obterSaldoUsuario();
  }, []);

  const fetchTransacoesPorData = async () => {
    try {
      const response = await transacaoAPI.buscarInformacoesTransacoesPorData(dataAtual);
      setDespesas(response.despesas);
      setReceitas(response.receitas);
    } catch (error) {
      console.error("Erro ao buscar transações por data:", error);
    }
  };

  useEffect(() => {
    fetchTransacoesPorData();
    obterSaldoUsuario();
  }, [dataAtual]);

  return (
    <SideBar>
      <div className={style.Header}>
        <h2>Transações</h2>
        <button onClick={() => setShowModalCriar(true)}>+ Transação</button>
      </div>

      <Card>
        <Card.Body className={style.cardDate}>
          <FiChevronLeft onClick={mesAnterior} />
          <div className="month-year-display">
            {currentMonth} de {currentYear}
          </div>
          <FiChevronRight onClick={proximoMes} />
        </Card.Body>
      </Card>

      <div className={style.cards}>
        <CardSaldo icon={<IoWalletOutline/>} saldo={saldoUsuario  } title="Saldo Atual"/>
        <CardSaldo icon={<FaArrowTrendUp/>} saldo={receitas} title="Receita do Mês"/>
        <CardSaldo icon={<FaArrowTrendDown/>} saldo={despesas} title="Despesas do Mês"/>
      </div>

      <Grafico labels={['opa,eita']} series={[1,6]}/>

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
              <td className={item.tipoTransacao === 'Receita' ? style.receita : style.despesa}>
                {item.tipoTransacao === 'Receita' ? '+' : '-'}{Math.abs(item.valor).toFixed(2)}
              </td>
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
            <Form.Group className="mb-3">
              <ToggleButtonGroup
                type="radio"
                name="tipoTransacao"
                value={tipoTransacao}
                onChange={handleTipoTransacaoChange}
              >
                <ToggleButton
                  id="tipo-receita"
                  value="Receita"
                  variant={tipoTransacao === 'Receita' ? 'success' : 'outline-success'}
                >
                  Receita
                </ToggleButton>
                <ToggleButton
                  id="tipo-despesa"
                  value="Despesa"
                  variant={tipoTransacao === 'Despesa' ? 'danger' : 'outline-danger'}
                >
                  Despesa
                </ToggleButton>
              </ToggleButtonGroup>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Descrição</Form.Label>
              <Form.Control 
                type="text" 
                value={descricao} 
                onChange={(e) => setDescricao(e.target.value)} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Valor</Form.Label>
              <Form.Control 
                type="number" 
                min="0.01" 
                step="0.01" 
                value={valor || ''} 
                onChange={(e) => setValor(parseFloat(e.target.value) || 0)} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Data</Form.Label>
              <Form.Control 
                type="date" 
                value={dataTransacao} 
                onChange={(e) => setDataTransacao(e.target.value)} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Categoria</Form.Label>
              <Form.Select 
                value={categoriaId} 
                onChange={(e) => setCategoriaId(Number(e.target.value))}
              >
                <option value={0}>Selecione uma categoria</option>
                {categoriasFiltradas.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.nome}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label>Forma de Pagamento</Form.Label>
              <Form.Select 
                value={formaPagamentoId} 
                onChange={(e) => setFormaPagamentoId(Number(e.target.value))}
              >
                <option value={0}>Selecione uma forma de pagamento</option>
                {formasPagamento.map((item) => (
                  <option key={item.id} value={item.id}>
                    {item.nome}
                  </option>
                ))}
              </Form.Select>
            </Form.Group>
          </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={cadastrarTransacao}>
            Adicionar
          </Button>
          <Button variant="danger" onClick={() => setShowModalCriar(false)}>
            Fechar
          </Button>
        </Modal.Footer>
      </Modal>
    </SideBar>
  );
};