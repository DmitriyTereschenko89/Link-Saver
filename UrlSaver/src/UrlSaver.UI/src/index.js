import React from "react";
import ReactDOM from 'react-dom';
import AppRoot from './components/app-root/app-root.js';
import { UrlClient } from "./clients/app-url-client.ts";
import '@mantine/core/styles.css';
import './index.css'

window.onload = () => {
    const curUrl = window.location.href.split('/');
    if (curUrl[curUrl.length - 1] === '') {
        return;
    }

    let urlClient = new UrlClient();
    urlClient.get(curUrl[curUrl.length - 1])
        .then((response) => {
            console.log(response.url);
            if (response.url) {
                window.location.replace(response.url);
            }
        })
}

ReactDOM.render(<AppRoot />, document.getElementById('root'));