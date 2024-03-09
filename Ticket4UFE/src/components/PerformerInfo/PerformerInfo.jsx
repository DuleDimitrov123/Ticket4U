import {
  Text,
  Stack,
  Flex,
  Box,
  IconButton,
  useDisclosure,
} from "@chakra-ui/react";
import styles from "./PerformerInfo.styles";
import usePerformers from "../../hooks/usePerformers";
import { BiEdit } from "react-icons/bi";
import PerformerModal from "../PerformerModal/PerformerModal";

const PerformerInfo = ({ selectedPerformerId }) => {
  const { performerData, performerLoading } =
    usePerformers(selectedPerformerId);
  const { isOpen, onOpen, onClose } = useDisclosure();

  const handleEditClick = () => {
    onOpen();
  };

  return (
    <Flex {...styles.aboutPerformerCard} position="relative">
      {selectedPerformerId && (
        <IconButton
          icon={<BiEdit />}
          aria-label="Edit"
          position="absolute"
          top="2"
          right="2"
          onClick={handleEditClick}
        />
      )}

      {selectedPerformerId ? (
        <Stack spacing="3">
          <Flex align="center">
            <Text ml={2} fontSize="xl" color="gray.500" fontWeight={"bold"}>
              {performerData?.name}
            </Text>
          </Flex>
          {performerData?.performerInfos?.map((info, index) => (
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
      ) : (
        <Box w="100%" h="100%" textAlign={"center"}>
          <Text>No selected performer</Text>
        </Box>
      )}
      {isOpen && (
        <PerformerModal
          isOpen={isOpen}
          onClose={onClose}
          isEditFlow
          performer={performerData}
        />
      )}
    </Flex>
  );
};

export default PerformerInfo;
