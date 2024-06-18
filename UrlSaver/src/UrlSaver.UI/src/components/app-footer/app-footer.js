import LabelFooter from '../ui-components/label-footer';
import '../app-block/app-block.css';
import '../app-footer/app-footer.css';
import { Flex } from '@mantine/core';

const AppFooter = () => {
    return (
        <Flex
            justify={'center'}
            align={'center'}
            className='app-block footer'>
            <LabelFooter />
        </Flex>
    );
};

export default AppFooter;