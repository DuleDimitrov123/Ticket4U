import Navbar from "../../components/Navbar";
import { Flex } from "@chakra-ui/react";

const PublicLayout = ({ overflow, children }) => {
  return (
    <Flex flexDir="column" w="100vw" h="100vh" overflow="hidden">
      <Navbar />
      <Flex flex={1} overflow={overflow ? overflow : "auto"}>
        {children}
      </Flex>
    </Flex>
  );
};

export default PublicLayout;
