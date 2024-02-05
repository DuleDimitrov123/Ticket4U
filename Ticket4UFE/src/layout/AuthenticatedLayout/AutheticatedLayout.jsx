import React from "react";
import { Flex } from "@chakra-ui/react";
import AuthenticatedNavbar from "../../components/Navbar/AuthenticatedNavbar";
import AuthenticatedSidebar from "../../components/Sidebar/AuthenticatedSidebar";
import { Box } from "@chakra-ui/react";
import styles from "./AuthenticatedLayout.styles";

const AuthenticatedLayout = ({ children }) => {
  return (
    <Flex w="100vw" h="100vh" overflow="hidden">
      <AuthenticatedSidebar />

      <Flex flexDir="column" flexGrow={1} h={"100%"}>
        <AuthenticatedNavbar />
        {/* <Scrollbars style={{ height: '100%' }}> */}
        <Box {...styles.authenticatedBox}>{children}</Box>
        {/* </Scrollbars> */}
      </Flex>
    </Flex>
  );
};

export default AuthenticatedLayout;
