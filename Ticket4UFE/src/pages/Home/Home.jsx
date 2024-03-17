import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import PublicLayout from "../../layout/PublicLayout/PublicLayout";
import { Flex, Spacer, Stack, Text, VStack } from "@chakra-ui/react";
import styles from "./Home.styles";
import { Link } from "react-router-dom";

const Home = () => {
  const auth = useContext(AuthContext);
  return (
    <PublicLayout overflow={"hidden"}>
      <Flex {...styles.wrapper} bgImage="url(/images/Background.jpg)">
        <VStack {...styles.vStack}>
          <Stack {...styles.content}>
            <Text {...styles.title}>Best place to book your tickets!</Text>
          </Stack>
        </VStack>
        <Spacer />
        <VStack {...styles.vStack}>
          <Stack {...styles.content}>
            <Text {...styles.title}>
              Click{" "}
              <Text
                color="purple.500"
                as={Link}
                to="/shows"
                textDecoration="underline"
              >
                here
              </Text>{" "}
              for shows
            </Text>
          </Stack>
        </VStack>
      </Flex>
    </PublicLayout>
  );
};

export default Home;
