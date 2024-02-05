import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import PublicLayout from "../../layout/PublicLayout/PublicLayout";
import { Flex, Stack, Text, VStack } from "@chakra-ui/react";
import styles from "./Home.styles";

const Home = () => {
  const auth = useContext(AuthContext);
  return (
    <PublicLayout>
      <Flex {...styles.wrapper} bgImage="url(/images/Background.jpg)">
        <VStack {...styles.vStack}>
          <Stack {...styles.content}>
            <Text {...styles.title}>Best place to book your tickets!</Text>
          </Stack>
        </VStack>
      </Flex>
    </PublicLayout>
  );
};

export default Home;
