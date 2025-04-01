import style from './Sidebar.module.css';
import { MdDashboard } from "react-icons/md";
import { FaFolderPlus } from "react-icons/fa";
import { FaPlusCircle } from "react-icons/fa";
import { FaCreditCard } from "react-icons/fa";


import logo from '../../assets/logo.svg';
import { SidebarElemento } from '../SidebarElemento/SidebarElemento';
interface ISideBarProps {
    children: React.ReactNode;
}

export const SideBar: React.FC<ISideBarProps> = ({children}) => 
    {
        return(
            <>
                <aside className={style.sidebar}>
                    <div className={style.sidebar_header}>
                        <img src={logo} alt="logo cashSmart" className={style.logo} />
                        <hr className={style.linha} />
                    </div>
                    <div className={style.sidebar_elementos}>
                        <SidebarElemento texto='Home' icone={<MdDashboard/>} link='/home'/>
                        <SidebarElemento texto='Transação' icone={<FaPlusCircle/>} link='/transacao'/>
                        <SidebarElemento texto='Categorias' icone={<FaFolderPlus/>} link='/categorias'/>
                        <SidebarElemento texto='Metódos de pagamento' icone={<FaCreditCard/>} link='/Metodos_pagamentos'/>
                    </div>

                </aside>
                <main className={style.conteudo}>
                    {children}  
                </main>
            </>
        )
}