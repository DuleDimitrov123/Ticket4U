import { Text, Flex, Card, CardBody, Box, Button } from "@chakra-ui/react";
import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext";

const ShowMessages = ({ showMessages, onAddMessageOpen }) => {
  const auth = useContext(AuthContext);
  const { isAdmin } = auth;
  return (
    <Flex w={"100%"} display={"flex"} flexDir={"column"} gap="4">
      {isAdmin() ? (
        <Flex justifyContent={"space-between"} alignItems={"center"}>
          <Text size="md" fontWeight={"500"}>
            MESSAGES{" "}
          </Text>
          <Button colorScheme="purple" onClick={() => onAddMessageOpen()}>
            Add message
          </Button>
        </Flex>
      ) : (
        <Text size="md" fontWeight={"500"}>
          MESSAGES{" "}
        </Text>
      )}

      {showMessages.length ? (
        <Box w="100%">
          {showMessages?.map((message, index) => (
            <Card key={index} w="100%" mb="4">
              <CardBody>
                <Text fontStyle={"italic"}>{message.value}</Text>
              </CardBody>
            </Card>
          ))}
        </Box>
      ) : (
        <Box>
          <Text size={"sm"} color="gray.500">
            There is no messages for this show.
          </Text>
        </Box>
      )}
    </Flex>
  );
};

export default ShowMessages;
