import { Heading, Text, Button, VStack } from "@chakra-ui/react";
import { Link } from "react-router-dom";
import styles from "./NotFound.styles";

const NotFound = () => {
  return (
    <VStack {...styles.wrapper}>
      <Heading as="h2" {...styles.heading}>
        404
      </Heading>
      <Text {...styles.title}>Page Not Found</Text>
      <Text {...styles.subtitle}>
        The page you're looking for does not seem to exist
      </Text>

      <Button as={Link} to="/" {...styles.button}>
        Go to Home
      </Button>
    </VStack>
  );
};

export default NotFound;
