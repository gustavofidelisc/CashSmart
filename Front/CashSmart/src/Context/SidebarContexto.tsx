import React, { use, useContext } from "react";


interface ISidebarElementoProps{
    expanded: boolean;
    toggleSidebar: () => void;
    closeSidebar: () => void;
}


const SidebarContexto = React.createContext({} as ISidebarElementoProps);

export const SidebarProvider: React.FC<React.PropsWithChildren> = ({children}) => {
    const [expanded, setExpanded] = React.useState(false);

    const toggleSidebar = () => {
        setExpanded(!expanded);
    };

    const closeSidebar = () => {
        setExpanded(false);
    };

    return (
        <SidebarContexto.Provider value={{ expanded, toggleSidebar, closeSidebar }}>
            {children}
        </SidebarContexto.Provider>
    );
};

export const useSidebarContexto = () => useContext(SidebarContexto);