import style from './AcessHeader.module.css';
import { Button } from 'react-bootstrap';
import logo from '../../assets/logo.svg';

interface AcessHeaderProps {
    navigateTo: string;
    textButton: string;
}

export const AcessHeader: React.FC<AcessHeaderProps> = ({ navigateTo, textButton }) => {
    const handleNavigation = () => {
        window.location.href = navigateTo; // Redirecionamento sem react-router
    };

    return (
        <header className={style.container_header}>
            <div className={style.container_logo}>
                <img src={logo} alt="logo cashSmart" />
            </div>
            <Button
                className={style.button_header}
                variant="primary"
                onClick={handleNavigation}>
                {textButton}
            </Button>
        </header>
    );
};
