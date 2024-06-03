import AppFooter from "./app-footer";
import AppMain from './app-main';
import AppHeader from "./app-header";
import './app-root.css';

const AppRoot = () => {
    return (
        <div className="app-root">
            <AppHeader />
            <AppMain />
            <AppFooter />
        </div>
    )
}

export default AppRoot;