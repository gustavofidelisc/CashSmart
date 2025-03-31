import { Link } from 'react-router-dom';
import style from './SidebarElemento.module.css';

interface ISidebarElementoProps {
    texto: string;
    icone: React.ReactNode;
    link: string;
}

export const SidebarElemento: React.FC<ISidebarElementoProps> = ({texto, icone, link}) => {
    return (
      <Link to={link} className={style.sidebar_elemento}>
        {icone}
        <h3 className={style.texto}>{texto}</h3>
      </Link>
    )
}