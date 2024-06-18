import AppFooter from "../app-footer/app-footer";
import AppMain from '../app-main/app-main';
import AppHeader from "../app-header/app-header";
import './app-root.css';
import { MantineProvider } from "@mantine/core";

const AppRoot = () => {
    return (
      <MantineProvider>
        <div className="app-root">
          <AppHeader />
          <AppMain />
          <AppFooter />
        </div>
      </MantineProvider>
    )
}

export default AppRoot;