import {
  Button,
  Divider,
  Flex,
  Heading,
  Icon,
  Spacer,
  useDisclosure,
} from "@chakra-ui/react";
import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import styles from "./Performers.styles";
import { BiLoader, BiPlus } from "react-icons/bi";
import usePerformers from "../../hooks/usePerformers";
import PerformersTable from "../../components/PerformersTable";
import { useState } from "react";
import PerformerInfo from "../../components/PerformerInfo";
import PerformerModal from "../../components/PerformerModal/PerformerModal";

const Performers = () => {
  const userInfo = localStorage.getItem("userInfo");
  const userInfoJSON = JSON.parse(userInfo);
  const { performers, performersLoading, refetchPerformers } = usePerformers();
  const [selectedPerformerId, setSelectedPerformerId] = useState(null);
  const { isOpen, onOpen, onClose } = useDisclosure();

  return (
    <AuthenticatedLayout>
      {performersLoading ? (
        <Flex w="100%" alignItems={"center"} justifyContent={"center"} h="100%">
          <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
        </Flex>
      ) : (
        <>
          {" "}
          <Flex {...styles.headerBox}>
            <Heading as="h3" size="lg" color="purple.500">
              Performers
            </Heading>
            <Spacer />
            <Button
              variant="solid"
              colorScheme="purple"
              className="view_details"
              onClick={onOpen}
              width={"100px"}
              leftIcon={<BiPlus />}
            >
              Add
            </Button>
          </Flex>
          <Flex w="100%" gap="4" h="50%">
            <PerformersTable
              performers={performers}
              setSelectedPerformerId={setSelectedPerformerId}
              selectedPerformerId={selectedPerformerId}
            />

            <Divider orientation="vertical" />

            <PerformerInfo selectedPerformerId={selectedPerformerId} />
            {isOpen && <PerformerModal isOpen={isOpen} onClose={onClose} />}
          </Flex>
        </>
      )}
    </AuthenticatedLayout>
  );
};
export default Performers;
