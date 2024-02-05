import { Box, Flex, Heading, Stack, Text } from "@chakra-ui/react";
import styles from "./SignupLayout.styles";

// eslint-disable-next-line react/prop-types
const SignupLayout = ({ title, subtitle, children }) => {
  return (
    <Flex {...styles.pageWrapper}>
      <Stack {...styles.contentWrapper}>
        <Box {...styles.card}>
          <Stack align={"center"} gap={1}>
            <Heading fontSize={"4xl"}>{title}</Heading>

            <Text fontSize={"md"} color={"gray.600"}>
              {subtitle}
            </Text>
          </Stack>
          {children}
        </Box>
      </Stack>
    </Flex>
  );
};

export default SignupLayout;
