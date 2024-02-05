import React from "react";
import { Box, Flex, Text } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import { BiSwim, BiDish, BiChalkboard, BiUser } from "react-icons/bi";
import { GiTheater } from "react-icons/gi";

import styles from "./AuthenticatedSidebar.styles";
import useUser from "../../hooks/useUser";
import { BsTicketPerforated } from "react-icons/bs";

const AuthenticatedSidebar = () => {
  const { user } = useUser();
  return (
    <Flex>
      <Box as="aside" {...styles.sideBar}>
        <Box marginBottom={"6"}>
          <Text size="xl" color="purple.500" fontWeight="700">
            Ticket4U
          </Text>
        </Box>
        <Flex {...styles.sideBarButton} as={NavLink} to="/shows">
          <GiTheater size={20} />
          <Text>Shows</Text>
        </Flex>
        <Flex {...styles.sideBarButton} as={NavLink} to="/reservations">
          <BsTicketPerforated size={20} />
          <Text>Reservations</Text>
        </Flex>
        {/* <Flex {...styles.sideBarButton} as={NavLink} to="/conference">
          <BiChalkboard size={20} />
          <Text>Conference</Text>
        </Flex>
        <Flex {...styles.sideBarButton} as={NavLink} to="/">
          <BiDish size={20} />
          <Text>Kitchen</Text>
        </Flex>
        {user.role === "admin" && (
          <Flex {...styles.sideBarButton} as={NavLink} to="/admin">
            <BiUser size={20} />
            <Text>Admin</Text>
          </Flex>
        )} */}
      </Box>
    </Flex>
  );
};

export default AuthenticatedSidebar;
