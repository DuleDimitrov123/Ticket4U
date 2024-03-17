import { Text, Stack, Flex } from "@chakra-ui/react";
import styles from "./AboutPerformer.styles";

const AboutPerformer = ({ performer }) => {
  return (
    <Flex {...styles.aboutPerformerCard}>
      <Stack mt="6" spacing="3">
        <Flex align="center">
          <Text ml={2} fontSize="sm" color="black" fontWeight={"500"}>
            Name:
          </Text>
          <Text ml={2} fontSize="sm" color="gray.500">
            {performer.name}
          </Text>
        </Flex>
        {performer?.performerInfos?.map((info, index) => (
          <Flex align="center" key={index}>
            <Text ml={2} fontSize="sm" color="black" fontWeight={"500"}>
              {info.name}:
            </Text>
            <Text ml={2} fontSize="sm" color="gray.500">
              {info.value}
            </Text>
          </Flex>
        ))}
      </Stack>
    </Flex>
  );
};

export default AboutPerformer;
