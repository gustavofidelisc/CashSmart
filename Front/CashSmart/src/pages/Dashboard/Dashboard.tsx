import { useEffect, useState } from "react";
import { SideBar } from "../../components/Sidebar/Sidebar";
import style from "./Dashboard.module.css";
import { Button, Card, Form, Modal, Table, ToggleButton, ToggleButtonGroup } from "react-bootstrap";
import { categoriaAPI, ICategoriaResponse } from "../../services/categoriaAPI";
import { FormaPagamentoAPI, IFormaPagamentoResponse } from "../../services/formaPagamentoAPI";
import { transacaoAPI, ITransacaoCriar, ITransacaoResposta, ItransacaoInformacoesGrafico, ITransacaoAtualizar } from "../../services/TransacaoAPI";
import { CardSaldo } from "../../components/Card/CardSaldo";
import { Grafico } from "../../components/Graficos/Grafico";
import { FiChevronLeft, FiChevronRight } from "react-icons/fi";
import { FaArrowTrendDown, FaArrowTrendUp } from "react-icons/fa6";
import { MdAttachMoney } from "react-icons/md";
import { IoWalletOutline } from "react-icons/io5";
import { FaArrowUp, FaArrowDown } from "react-icons/fa";
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';

dayjs.extend(utc);

export const Dashboard: React.FC = () => {
  // Estados do componente
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState<number>(0);
  const [dataTransacao, setDataTransacao] = useState("");
  const [categoriaId, setCategoriaId] = useState<number>(0);
  const [formaPagamentoId, setFormaPagamentoId] = useState<number>(0);
  const [tipoTransacao, setTipoTransacao] = useState<'Receita' | 'Despesa'>('Despesa');
  const [despesas, setDespesas] = useState<number>(0);
  const [receitas, setReceitas] = useState<number>(0);
  const [saldoUsuario, setSaldoUsuario] = useState<number>(0);
  const [showModalCriar, setShowModalCriar] = useState(false);
  const [showModalAtualizar, setShowModalAtualizar] = useState(false);
  const [showConfirmDelete, setShowConfirmDelete] = useState(false);
  const [transacaoAtualizar, setTransacaoAtualizar] = useState<ITransacaoResposta>({
    id: 0,
    descricao: "",
    valor: 0,
    data: "",
    tipoTransacao: "",
    nomeCategoria: "",
    nomeFormaPagamento: "",
    categoriaId: 0,
    formaPagamentoId: 0
  });
  const [transacoes, setTransacoes] = useState<ITransacaoResposta[]>([]);
  const [categorias, setCategorias] = useState<ICategoriaResponse[]>([]);
  const [categoriasFiltradas, setCategoriasFiltradas] = useState<ICategoriaResponse[]>([]);
  const [formasPagamento, setFormasPagamento] = useState<IFormaPagamentoResponse[]>([]);
  const [informacoesGrafico, setInformacoesGrafico] = useState<ItransacaoInformacoesGrafico>({
    categoriaNomes: [],
    valores: [],
    tipoTransacao: []
  });
  const [tipoTransacaoGrafico, setTipoTransacaoGrafico] = useState<number>(1);
  const [carregandoGrafico, setCarregandoGrafico] = useState(false);
  const [dataAtual, setDataAtual] = useState(dayjs().startOf('month'));

  const meses = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
  const currentMonth = meses[dataAtual.month()];
  const currentYear = dataAtual.year();

  // Funções de navegação entre meses
  const proximoMes = () => setDataAtual(dataAtual.add(1, 'month').startOf('month'));
  const mesAnterior = () => setDataAtual(dataAtual.subtract(1, 'month').startOf('month'));

  // Funções para buscar dados da API
  const listarTransacoesPorData = async () => {
    try {
      const response = await transacaoAPI.listarTransacoesPorData(dataAtual);
      setTransacoes(response);
    } catch (error) {
      console.error("Erro ao listar transações:", error);
    }
  };

  const listarCategorias = async () => {
    try {
      const response = await categoriaAPI.listarCategorias();
      setCategorias(response);
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

  const buscarInformacoesTransacoesPorData = async () => {
    try {
      const response = await transacaoAPI.buscarInformacoesTransacoesPorData(dataAtual);
      setDespesas(response.despesas);
      setReceitas(response.receitas);
    } catch (error) {
      console.error("Erro ao buscar transações por data:", error);
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

  const obterInformacoesGrafico = async () => {
    setCarregandoGrafico(true);
    try {
      const response = await transacaoAPI.obterInformacoesGraficoPorData(dataAtual, tipoTransacaoGrafico);
      
      const valoresPositivos = response.valores
        .map(v => Math.abs(typeof v === 'string' ? parseFloat(v) : Number(v)))
        .filter(v => !isNaN(v));
      
      setInformacoesGrafico({
        categoriaNomes: [...response.categoriaNomes],
        valores: valoresPositivos,
        tipoTransacao: [...(response.tipoTransacao || [])]
      });
    } catch (error) {
      console.error("Erro ao obter informações do gráfico:", error);
      setInformacoesGrafico({
        categoriaNomes: [],
        valores: [],
        tipoTransacao: []
      });
    } finally {
      setCarregandoGrafico(false);
    }
  };

  // Funções de manipulação de dados
  const handleTipoTransacaoChange = (tipo: 'Receita' | 'Despesa') => {
    setTipoTransacao(tipo);
    setCategoriaId(0);
    setCategoriasFiltradas(categorias.filter(c => c.tipoTransacao === tipo));
  };

  const handleTransacaoAtualizar = (transacao: ITransacaoResposta) => {
    setTransacaoAtualizar(transacao);
    setShowModalAtualizar(true);
  }

  const handleTipoTransacaoChangeGrafico = (novoTipo: number) => {
    setTipoTransacaoGrafico(novoTipo);
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
      listarTransacoesPorData();
      buscarInformacoesTransacoesPorData();
      obterSaldoUsuario();
      obterInformacoesGrafico();
      setShowModalCriar(false);
    } catch (error) {
      console.error("Erro ao cadastrar transação:", error);
      alert("Erro ao cadastrar transação.");
    }
  };

  const atualizarTransacao = async () => {
    try {
      await transacaoAPI.atualizarTransacao(transacaoAtualizar);
      setTransacaoAtualizar({
        id: 0,
        descricao: "",
        valor: 0,
        data: "",
        tipoTransacao: "",
        nomeCategoria: "",
        nomeFormaPagamento: "",
        categoriaId: 0,
        formaPagamentoId: 0
      });
      setShowModalAtualizar(false);
      listarTransacoesPorData();
      buscarInformacoesTransacoesPorData();
      obterSaldoUsuario();
      obterInformacoesGrafico();
    } catch (error) {
      console.error("Erro ao atualizar transação:", error);
      alert("Erro ao atualizar transação.");
    }
  }

  const handleDeleteClick = (transacao: ITransacaoResposta) => {
    setTransacaoAtualizar(transacao);
    setShowConfirmDelete(true);
  };

  const confirmDelete = async () => {
    try {
      await transacaoAPI.deletarTransacao(transacaoAtualizar.id);
      setTransacoes(transacoes.filter(t => t.id !== transacaoAtualizar.id));
      setShowConfirmDelete(false);
      setShowModalAtualizar(false);
      buscarInformacoesTransacoesPorData();
      obterSaldoUsuario();
      obterInformacoesGrafico();
    } catch (error) {
      console.error("Erro ao deletar transação:", error);
      alert("Erro ao deletar transação.");
    }
  };

  // Effects
  useEffect(() => {
    listarTransacoesPorData();
    listarCategorias();
    listarFormasPagamento();
    obterSaldoUsuario();
    obterInformacoesGrafico();
  }, []);

  useEffect(() => {
    listarTransacoesPorData();
    buscarInformacoesTransacoesPorData();
    obterSaldoUsuario();
    obterInformacoesGrafico();
  }, [dataAtual]);

  useEffect(() => {
    obterInformacoesGrafico();
  }, [tipoTransacaoGrafico]);

  return (
    <SideBar>
      <div className={style.Header}>
        <h2>Dashboard</h2>
        <button onClick={() => setShowModalCriar(true)}>+ Transação</button>
      </div>

      <div className={style.container}>
        <Card className={style.card_container}>
          <Card.Body className={style.cardDate}>
            <FiChevronLeft className={style.icon} onClick={mesAnterior} />
            <div className={style.dataAtual}>
              {currentMonth} de {currentYear}
            </div>
            <FiChevronRight className={style.icon} onClick={proximoMes} />
          </Card.Body>
        </Card>

        <div className={style.cards}>
          <CardSaldo icon={<IoWalletOutline/>} saldo={saldoUsuario} title="Saldo Atual"/>
          <CardSaldo icon={<FaArrowTrendUp/>} saldo={receitas} title="Receita do Mês"/>
          <CardSaldo icon={<FaArrowTrendDown/>} saldo={despesas} title="Despesas do Mês"/>
        </div>

        {carregandoGrafico ? (
          <div className={style.loading}>Carregando gráfico...</div>
        ) : transacoes.length <= 0 ? (
          <div className={style.semDados}>
            <MdAttachMoney size={50} />
            <h3>Sem transações registradas</h3>
          </div>
        ) : (
          <>
            <div className={style.grafico}>
              <div className={style.header_grafico}>
                <h2>Categorias</h2>
                <div className={style.grafico_buttons}>
                  <ToggleButtonGroup
                    type="radio"
                    name="tipoTransacaoGrafico"
                    value={tipoTransacaoGrafico}
                    onChange={handleTipoTransacaoChangeGrafico}
                  >
                    <ToggleButton
                      id="receita"
                      value={1}
                      variant={tipoTransacaoGrafico === 1 ? 'success' : 'outline-success'}
                    >
                      Receita
                    </ToggleButton>
                    <ToggleButton
                      id="despesa"
                      value={2}
                      variant={tipoTransacaoGrafico === 2 ? 'danger' : 'outline-danger'}
                    >
                      Despesa
                    </ToggleButton>
                  </ToggleButtonGroup>
                </div>
              </div>
              {informacoesGrafico.valores.length > 0 ? (
                <Grafico
                  key={`grafico-${tipoTransacaoGrafico}-${dataAtual.format('YYYY-MM')}`}
                  labels={informacoesGrafico.categoriaNomes}
                  series={informacoesGrafico.valores}
                />
              ) : (
                <div className={style.semDados}>
                  <MdAttachMoney size={50} />
                  <h3>Sem transações registradas</h3>
                </div>
              )}
            </div>

            <div className={style.tabela}>
              <h2>Transações Do mês</h2>
              {transacoes.map((transacao) => (
                <Card key={transacao.id} className={style.cardTransacao} onClick={() => handleTransacaoAtualizar(transacao)}>
                  <Card.Body className={style.cardBody}>
                    <div className={style.cardInfo}>
                      {transacao.valor <= 0 ? <FaArrowDown className={style.arrow_down} /> : <FaArrowUp className={style.arrow_up} />}
                      <div className={style.cardText}>
                        <Card.Title>{`${transacao.nomeCategoria} / ${transacao.nomeFormaPagamento}`}</Card.Title>
                        <Card.Text className={style.text_reduzido}>{transacao.descricao}</Card.Text> 
                      </div>
                    </div>
                    <div className={style.cardInfo}>
                      <div className={style.cardText}>
                        <Card.Title style={{ color: transacao.valor > 0 ? '#28a745' : '#dc3545' }}>
                          {transacao.valor.toLocaleString('pt-BR', { 
                            style: 'currency', 
                            currency: 'BRL' 
                          })}
                        </Card.Title>
                        <Card.Text className={style.text}>{dayjs(transacao.data).format('DD/MM/YYYY')}</Card.Text>
                      </div>
                    </div>
                  </Card.Body>
                </Card>
              ))}
            </div>
          </>
        )}
      </div>

      {/* Modal de Criação */}
      <Modal show={showModalCriar} onHide={() => setShowModalCriar(false)}>
        <Modal.Header closeButton>
          <Modal.Title className="text-center w-100">Adicionar Transação</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3 d-flex justify-content-center align-items-center">
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
              <Form.Label className="fs-2">Descrição</Form.Label>
              <Form.Control 
                className="fs-3" 
                type="text" 
                as="textarea"
                rows={2}
                value={descricao} 
                onChange={(e) => setDescricao(e.target.value)} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Valor</Form.Label>
              <Form.Control 
                className="fs-3" 
                type="number" 
                min="0.01" 
                step="0.01" 
                value={valor || ''} 
                onChange={(e) => setValor(parseFloat(e.target.value) || 0)} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Data</Form.Label>
              <Form.Control 
                className="fs-3" 
                type="date" 
                value={dataTransacao} 
                onChange={(e) => setDataTransacao(e.target.value)} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Categoria</Form.Label>
              <Form.Select 
                className="fs-3"
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
              <Form.Label className="fs-2">Forma de Pagamento</Form.Label>
              <Form.Select 
                className="fs-2"
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

      {/* Modal de Atualização */}
      <Modal show={showModalAtualizar} onHide={() => setShowModalAtualizar(false)}>
        <Modal.Header closeButton>
          <Modal.Title className="text-center w-100">Editar Transação</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form>
            <Form.Group className="mb-3 d-flex justify-content-center align-items-center">
              <ToggleButtonGroup
                type="radio"
                name="tipoTransacao"
                value={transacaoAtualizar.tipoTransacao}
                onChange={(val: 'Receita' | 'Despesa') => setTransacaoAtualizar({ 
                  ...transacaoAtualizar, 
                  tipoTransacao: val 
                })}
              >
                <ToggleButton
                  id="tipo-receita"
                  value="Receita"
                  variant={transacaoAtualizar.tipoTransacao === 'Receita' ? 'success' : 'outline-success'}
                >
                  Receita
                </ToggleButton>
                <ToggleButton
                  id="tipo-despesa"
                  value="Despesa"
                  variant={transacaoAtualizar.tipoTransacao === 'Despesa' ? 'danger' : 'outline-danger'}
                >
                  Despesa
                </ToggleButton>
              </ToggleButtonGroup>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Descrição</Form.Label>
              <Form.Control 
                className="fs-3" 
                type="text" 
                as="textarea"
                rows={5}
                value={transacaoAtualizar.descricao} 
                onChange={(e) => setTransacaoAtualizar({ 
                  ...transacaoAtualizar, 
                  descricao: e.target.value 
                })} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Valor</Form.Label>
              <Form.Control 
                className="fs-3" 
                type="number" 
                min="0.01"
                step="0.01"
                value={Math.abs(transacaoAtualizar.valor) || 0.01} 
                onChange={(e) => setTransacaoAtualizar({ 
                  ...transacaoAtualizar, 
                  valor: parseFloat(e.target.value) || 0 
                })} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Data</Form.Label>
              <Form.Control 
                className="fs-3" 
                type="date" 
                value={dayjs(transacaoAtualizar.data).format('YYYY-MM-DD')} 
                onChange={(e) => setTransacaoAtualizar({ 
                  ...transacaoAtualizar, 
                  data: e.target.value 
                })} 
              />
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Categoria</Form.Label>
              <Form.Select 
                className="fs-2"
                value={transacaoAtualizar.categoriaId} 
                onChange={(e) => setTransacaoAtualizar({ 
                  ...transacaoAtualizar, 
                  categoriaId: Number(e.target.value) 
                })}
              >
                <option value={0}>Selecione uma categoria</option>
                {categorias
                  .filter(c => c.tipoTransacao === transacaoAtualizar.tipoTransacao)
                  .map((item) => (
                    <option key={item.id} value={item.id}>
                      {item.nome}
                    </option>
                  ))}
              </Form.Select>
            </Form.Group>

            <Form.Group className="mb-3">
              <Form.Label className="fs-2">Forma de Pagamento</Form.Label>
              <Form.Select 
                className="fs-2"
                value={transacaoAtualizar.formaPagamentoId} 
                onChange={(e) => setTransacaoAtualizar({ 
                  ...transacaoAtualizar, 
                  formaPagamentoId: Number(e.target.value) 
                })}
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
          <Button variant="danger" onClick={() => handleDeleteClick(transacaoAtualizar)}>
            Deletar
          </Button>
          <Button variant="success" onClick={atualizarTransacao}>
            Salvar Alterações
          </Button>
          <Button variant="secondary" onClick={() => setShowModalAtualizar(false)}>
            Fechar
          </Button>
        </Modal.Footer>
      </Modal>

      {/* Modal de Confirmação de Exclusão */}
      <Modal show={showConfirmDelete} onHide={() => setShowConfirmDelete(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title className="text-center w-100">Confirmar Exclusão</Modal.Title>
        </Modal.Header>
        <Modal.Body className="text-center">
          <p className="fs-4">Tem certeza que deseja excluir esta transação?</p>
          <p className="fw-bold">{transacaoAtualizar.descricao}</p>
          <p className="text-danger fs-5">
            {transacaoAtualizar.valor.toLocaleString('pt-BR', { 
              style: 'currency', 
              currency: 'BRL' 
            })}
          </p>
        </Modal.Body>
        <Modal.Footer className="justify-content-center">
          <Button variant="secondary" onClick={() => setShowConfirmDelete(false)}>
            Cancelar
          </Button>
          <Button variant="danger" onClick={confirmDelete}>
            Confirmar Exclusão
          </Button>
        </Modal.Footer>
      </Modal>
    </SideBar>
  );
};