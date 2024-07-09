import { useState } from 'react';
import '../app-form/app-form.css';
import '../css/custom-color.css';
import { UrlClient, UrlDto } from '../../clients/app-url-client.ts';
import { Text, Button, Dialog, Flex, Group, TextInput, Title, rem } from '@mantine/core';
import { useForm } from '@mantine/form';
import { IconChecks, IconCopy, IconExclamationCircle } from '@tabler/icons-react';

const AppForm = () => {
  const pattern = /(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?\/[a-zA-Z0-9]{2,}|((https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?)|(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?/g;
  const [ iconCopy, iconCopied ] = [ <IconCopy style={{width: rem(16), height: rem(16)}} />, <IconChecks style={{width: rem(16), height: rem(16)}} />];
  const [opened, setOpened] = useState(false);
  const [popupText] = ['An unexpected error occurred, please try again later.'];
  const [ copy, copied] = [ 'Copy', 'Copied'];   
  const [link, setLink] = useState('');
  const [copyImage, setCopyImage] = useState(iconCopy);
  const [copyText, setCopyText] = useState(copy);
  const linkInputHandler = (event) => {
    setLink(event.target.value);
  }

  const setCopyValues = (isCopy = true) => {
    setCopyImage(isCopy ? iconCopied : iconCopy);
    setCopyText(isCopy ? copied : copy);
  }

  const copyShortLink = async () => {
    await navigator.clipboard.writeText(link);
    setCopyValues();
    setTimeout(() => {
      setCopyValues(false);
    }, 1000);
  }
  
  const url = useForm({
    mode: 'uncontrolled',
    initialValues: { originalUrl: '' },
    validate: {
      originalUrl: (value) => (pattern.test(value) ? null : 'url is invalid')
    }
  });

  return (
    <Flex
      direction={'column'}
      flex={1}
      justify={'center'}
      align={'center'}
      gap={'md'}
      >
      <Dialog 
        bg={'red'}
        opened={opened}
        size={'md'} 
        radius={'md'}
        position={{ top: 20, right: 20}}
        transitionProps={{transform: 0, duration: 200}}
        style={{border:'0.5px solid rgba(232, 237, 242, 0.3)'}}>
        <Flex
          justify={'center'}>
          <IconExclamationCircle 
            style={{width: rem(30), height: rem(30), color: '#E8EDF2'}}/>
          <Text
            c={'#E8EDF2'} 
            align='center'
            size="sm" 
            mb="xs" 
            fw={500}>
              {popupText}
          </Text>
        </Flex>
      </Dialog>
      <Title 
        justify='flex-center'
        order={2} 
        size='h1'
        align='center'        
        className='custom-color'>Url Shortener</Title>
      <form 
        className='app-form' 
        onSubmit={url.onSubmit((values) => {
          let curUrl = values.originalUrl;
          if (!curUrl.startsWith('http') || !curUrl.startsWith('https')) {
            curUrl = 'http://' + url;
          }

          const urlDto = UrlDto.fromJS({ url: url });
          const urlClient = new UrlClient();
          urlClient.createShortUrl(urlDto)
            .then((response) => {
              setLink(`http://localhost:3000/${response.url}`);
             })
             .catch((event) => {
                if (event.status >= 400 && event.status < 500) {
                  url.setErrors({originalUrl: 'url is invalid'});
               }
               else {
                  setOpened(true);
                  setTimeout(() => {
                    setOpened(false);
                  }, 5000);
              }
            })
          })}>
        <Flex
          direction={'column'}>
          <TextInput
            c={'#E8EDF2'}
            variant='filled'
            onChange={linkInputHandler}
            placeholder="Enter a long URL to shorten"
            key={url.key('originalUrl')}
            {...url.getInputProps('originalUrl')}  
            mt='md'
          />
        </Flex>  
        <Group 
          justify="flex-center" 
          mt="md">
          <Button 
            fullWidth
            color='#1C1E20'
            type="submit" 
            fw={'normal'}
            style={{border: '1px solid rgba(232, 237, 242, 0.3)'}}
            size='compact-lg'>Shorten Url</Button>
        </Group>
      </form>
      <Flex
          justify={'center'}
          direction={'column'}
          w={'40%'}>
          <Flex
            justify={'space-between'}
            align={'center'}>
            <TextInput
              disabled
              value={link}
              variant='filled'
              className='custom-input'
              flex={'0.95'}
              placeholder='Your URL shortened'/>
              <Group>
                <Button 
                  fullWidth 
                  size='compact-lg'
                  color='#1C1E20'
                  justify='center'
                  w={'100px'}
                  onClick={copyShortLink}
                  leftSection={copyImage}
                  borderColor='#E8EDF2'
                  fw={'normal'}
                  style={{fontSize: '16px', border: '1px solid rgba(232, 237, 242, 0.3)'}}
                  type="submit">{copyText}</Button>
              </Group>
          </Flex>
        </Flex>
    </Flex>
  );
};

export default AppForm;