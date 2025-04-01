import style from './Dashboard.module.css';

import { SideBar } from '../../components/Sidebar/Sidebar';

export const Home: React.FC = () => {
    return (
        <SideBar>
            <div className={style.container}>
                <h1>Home</h1>
            </div>
        </SideBar>
    )
}