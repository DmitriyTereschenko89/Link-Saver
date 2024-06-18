import { Flex, Title } from "@mantine/core";

const WebName = () => {
    return (
       <Flex
        justify={"center"}
        align={"center"}>
        <Title order={1} size='h2' style={{marginLeft: "35px"}} className="custom-color">Url Shortener</Title>
       </Flex>
    );
};

export default WebName;