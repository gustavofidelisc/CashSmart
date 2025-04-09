import { useEffect, useState } from "react";
import { GraficoBarras } from "../../components/Graficos/GraficoBarras";
import { Sidebar } from "../../components/Sidebar/Sidebar";
import style from "./Relatorio.module.css";
import { transacaoAPI } from "../../services/TransacaoAPI";
import dayjs from "dayjs";
import { Button, Card, Modal, Spinner } from "react-bootstrap";
import { FiChevronLeft, FiChevronRight } from "react-icons/fi";
import { IAAPI } from "../../services/IAAPI";
import { marked } from "marked";

export const Relatorio: React.FC = () => {
    const [ano, setAno] = useState<number>(dayjs().year());
    const [receitas, setReceitas] = useState<number[]>([]);
    const [despesas, setDespesas] = useState<number[]>([]);
    const [saldos, setSaldos] = useState<number[]>([]);
    const [loading, setLoading] = useState<boolean>(false);

    const [showModalIA, setShowModalIA] = useState(false);
    const [respostaIA, setRespostaIA] = useState<string>("");
    const [loadingIA, setLoadingIA] = useState<boolean>(false);

    const obterInformacoesGrafico = async () => {
        setLoading(true);
        try {
            const response = await transacaoAPI.obterInformacoesGraficoPorAno(ano);
            setReceitas(response.receitas);
            setDespesas(response.despesas.map(despesa => despesa * -1));
            setSaldos(response.saldos);
        } catch (error) {
            console.error("Erro ao obter informações do gráfico:", error);
        } finally {
            setLoading(false);
        }
    };

    const obterAnaliseIA = async () => {
        setLoadingIA(true);
        setShowModalIA(true);
        try {
            const resposta = await IAAPI.obterInformacoesIA(ano);
            const html = await marked.parse(resposta.response); // converter Markdown para HTML
            setRespostaIA(html); // ajuste conforme o retorno real da API
        } catch (error) {
            console.error("Erro ao obter análise da IA:", error);
            setRespostaIA("Ocorreu um erro ao obter a análise da IA.");
        } finally {
            setLoadingIA(false);
        }
    };

    useEffect(() => {
        obterInformacoesGrafico();
    }, [ano]);

    const mesAnterior = () => setAno(ano - 1);
    const proximoMes = () => setAno(ano + 1);

    return (
        <Sidebar>
            <div className={style.container}>
                <div className={style.Header}>
                    <h2>Análise anual</h2>
                    <button onClick={obterAnaliseIA}>Análise IA</button>
                </div>

                <Card className={style.card_container}>
                    <Card.Body className={style.cardDate}>
                        <FiChevronLeft className={style.icon} onClick={mesAnterior} />
                        <div className={style.dataAtual}>{ano}</div>
                        <FiChevronRight className={style.icon} onClick={proximoMes} />
                    </Card.Body>
                </Card>

                {loading ? (
                    <div className={style.semDados}><h3>Carregando gráfico...</h3></div>
        ) : (
                    <div className={style.grafico}>
                        <h2>Saldo Anual Comparativos</h2>
                        <GraficoBarras 
                            key={`grafico-${ano}`}
                            despesas={despesas}
                            receitas={receitas}
                            saldos={saldos}
                        />
                    </div>
                )}
            </div>

            <Modal show={showModalIA} onHide={() => setShowModalIA(false)} centered size="lg">
                <Modal.Header closeButton>
                    <Modal.Title className="text-center w-100">Análise Inteligente</Modal.Title>
                </Modal.Header>
                <Modal.Body style={{ whiteSpace: "pre-line", fontSize: "1.1rem" }}>
                    {loadingIA ? (
                        <div className="d-flex justify-content-center align-items-center" style={{ height: "150px" }}>
                            <Spinner animation="border" variant="primary" className="me-2" />
                            Carregando análise da IA...
                        </div>
                    ) : (
                        <div
                            style={{ fontSize: "1.1rem" }}
                            dangerouslySetInnerHTML={{ __html: respostaIA }}
                        />
                    )}
                </Modal.Body>
                <Modal.Footer className="justify-content-center">
                    <Button variant="secondary" onClick={() => setShowModalIA(false)}>
                        Fechar
                    </Button>
                </Modal.Footer>
            </Modal>
        </Sidebar>
    );
};
