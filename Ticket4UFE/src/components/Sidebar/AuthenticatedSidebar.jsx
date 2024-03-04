import { useContext } from "react";
import { Box, Flex, Text } from "@chakra-ui/react";
import { NavLink } from "react-router-dom";
import { GiDramaMasks, GiTheater } from "react-icons/gi";
import styles from "./AuthenticatedSidebar.styles";
import { BsTicketPerforated } from "react-icons/bs";
import { AuthContext } from "../../context/AuthContext";
import { BiSolidCategory } from "react-icons/bi";

const AuthenticatedSidebar = () => {
  const { isAdmin } = useContext(AuthContext);
  return (
    <Flex>
      {isAdmin() ? (
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
          <Flex {...styles.sideBarButton} as={NavLink} to="/performers">
            <GiDramaMasks size={20} />
            <Text>Performers</Text>
          </Flex>
          <Flex {...styles.sideBarButton} as={NavLink} to="/categories">
            <BiSolidCategory size={20} />
            <Text>Categories</Text>
          </Flex>
          <Flex {...styles.sideBarButton} as={NavLink} to="/reservations">
            <BsTicketPerforated size={20} />
            <Text>Reservations</Text>
          </Flex>
        </Box>
      ) : (
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
        </Box>
      )}
    </Flex>
  );
};

export default AuthenticatedSidebar;
