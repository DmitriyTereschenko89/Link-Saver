import WebName from '../ui-components/web-name';
import '../app-block/app-block.css';
import '../app-header/app-header.css';
import { Flex } from '@mantine/core';

const AppHeader = () => {
    return (
        <Flex
            justify={'flex-start'}
            className='app-block header'>
            <WebName />
        </Flex>
    )
};

export default AppHeader;