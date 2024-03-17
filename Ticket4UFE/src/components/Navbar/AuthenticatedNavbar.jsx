import {
  Box,
  Flex,
  Button,
  Stack,
  Menu,
  MenuButton,
  Avatar,
  MenuList,
  MenuItem,
  Text,
  Icon,
} from "@chakra-ui/react";
import styles from "./Navbar.styles";
import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import { BiChevronDown } from "react-icons/bi";

const AuthenticatedNavbar = () => {
  const auth = useContext(AuthContext);
  const { isAdmin } = auth;
  const { firstName, lastName, role } = auth?.authState?.userInfo;

  return (
    <Box>
      <Flex {...styles.wrapper}>
        <Stack {...styles.avatarWrapper}>
          <Flex alignItems={"center"}>
            <Menu>
              <Flex alignItems={"center"}>
                <MenuButton
                  as={Button}
                  rounded={"full"}
                  variant={"link"}
                  cursor={"pointer"}
                  minW={0}
                  ml={"2.5"}
                >
                  <Flex alignItems={"center"} gap={"8px"}>
                    <Avatar
                      size={"sm"}
                      marginRight={"1"}
                      name={`${firstName} ${lastName}`}
                      bgColor={"green.200"}
                    />
                    <Flex
                      alignItems={"flex-start"}
                      flexDirection={"column"}
                      display={{ base: "none", md: "flex" }}
                      gap={"1"}
                    >
                      <Text fontSize="sm" color="gray.700">
                        {firstName + " " + lastName}
                      </Text>
                      {isAdmin() && <Text fontSize={"sm"}>Admin</Text>}
                      <Text
                        fontSize="xs"
                        color="gray.600"
                        fontWeight={"normal"}
                      >
                        {role}
                      </Text>
                    </Flex>
                    <Icon as={BiChevronDown} color={"gray.600"} />
                  </Flex>
                </MenuButton>
              </Flex>
              <MenuList>
                <MenuItem onClick={() => auth?.logout()}>Sign Out</MenuItem>
              </MenuList>
            </Menu>
          </Flex>
        </Stack>
      </Flex>
    </Box>
  );
};

export default AuthenticatedNavbar;
