
import style from "./Sidebar.module.css";
import { MdDashboard } from "react-icons/md";
import { CiLogout } from "react-icons/ci";
import { FaBars, FaFolderPlus, FaCreditCard } from "react-icons/fa";
import logo from "../../assets/logo.svg";
import { Nav } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { useAutenticacaoContexto } from "../../Context/AutenticacaoContexto";
import { useSidebarContexto } from "../../Context/SidebarContexto";

interface ISideBarProps {
    children: React.ReactNode;
}

export const SideBar: React.FC<ISideBarProps> = ({ children }) => {
    const { expanded, toggleSidebar, closeSidebar } = useSidebarContexto();
    const navigate = useNavigate();
    const { logout } = useAutenticacaoContexto();

    const handleNavigation = (path: string) => {
        navigate(path);
        if (window.innerWidth <= 768) {
            closeSidebar(); // Fecha a sidebar no mobile
        }
    };

    return (
        <>
            {/* Botão de abrir a sidebar no mobile */}
            {!expanded && (
                <button className={style.mobile_menu_button} onClick={toggleSidebar}>
                    <FaBars />
                </button>
            )}

            <div
                className={`${style.sidebar} ${
                    expanded ? style.sidebar_expanded : style.sidebar_collapsed
                }`}
            >
                <div className={style.sidebar_header}>
                    {expanded && (
                        <img
                            src={logo}
                            alt="logo cashSmart"
                            className={style.logo}
                        />
                    )}
                    <FaBars className={style.bars} onClick={toggleSidebar} />
                </div>
                <div className={style.row} />
                <Nav className={style.nav}>
                    <Nav.Link className={style.nav_link} onClick={() => handleNavigation("/Dashboard")}>
                        <MdDashboard className={style.icon} />
                        {expanded && <span className={style.text}>Dashboard</span>}
                    </Nav.Link>
                    <Nav.Link className={style.nav_link} onClick={() => handleNavigation("/Categorias")}>
                        <FaFolderPlus className={style.icon} />
                        {expanded && <span className={style.text}>Categorias</span>}
                    </Nav.Link>
                    <Nav.Link className={style.nav_link} onClick={() => handleNavigation("/Metodos_pagamentos")}>
                        <FaCreditCard className={style.icon} />
                        {expanded && <span className={style.text}>Métodos de Pagamento</span>}
                    </Nav.Link>
                </Nav>

                <div className={style.row} />
                <Nav className={style.nav_bottom}>
                    <Nav.Link className={style.nav_link} onClick={logout}>
                        <CiLogout className={style.icon} />
                        {expanded && <span className={style.text}>Sair</span>}
                    </Nav.Link>
                </Nav>
            </div>

            {/* Overlay para fechar a sidebar ao clicar fora */}
            {expanded && <div className={style.overlay} onClick={toggleSidebar}></div>}

            <main className={style.conteudo}>{children}</main>
        </>
    );
};
