import { Text, Flex, Card, CardBody, Box } from "@chakra-ui/react";

const ShowMessages = ({ showMessages }) => {
  return (
    <Flex w={"100%"} display={"flex"} flexDir={"column"} gap="4">
      <Text size="md" fontWeight={"500"}>
        MESSAGES{" "}
      </Text>
      {showMessages.length ? (
        <Box w="100%">
          {showMessages?.map((message, index) => (
            <Card key={index} w="100%">
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
