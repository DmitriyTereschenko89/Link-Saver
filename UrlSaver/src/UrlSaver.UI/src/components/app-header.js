import WebName from './web-name';
import './app-block.css';
import './app-header.css';

const AppHeader = () => {
    return (
        <div className="app-block header">
            <div>
                <WebName />
            </div>
        </div>
    )
};

export default AppHeader;