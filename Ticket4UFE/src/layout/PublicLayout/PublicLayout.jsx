import React from "react";
import Navbar from "../../components/Navbar";
import { Flex } from "@chakra-ui/react";

const PublicLayout = ({ children }) => {
  return (
    <Flex flexDir="column" w="100vw" h="100vh" overflow="hidden">
      <Navbar />
      <Flex flex={1}>{children}</Flex>
    </Flex>
  );
};

export default PublicLayout;
