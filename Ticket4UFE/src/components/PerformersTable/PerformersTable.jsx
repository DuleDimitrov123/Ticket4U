import {
  TableContainer,
  Table,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
  Button,
  useDisclosure,
} from "@chakra-ui/react";
import styles from "./PerformersTable.styles";
import { useState } from "react";

const PerformersTable = ({
  performers,
  selectedPerformerId,
  setSelectedPerformerId,
}) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [selectedPerformer, setSelectedPerformer] = useState({});
  const {
    isOpen: isDeleteModalOpen,
    onOpen: onDeleteModalOpen,
    onClose: onDeleteModalClose,
  } = useDisclosure();

  const editPerformer = (performer) => {
    setSelectedPerformer(performer);
    onOpen();
  };

  return (
    <TableContainer w="100%" maxH="300px" overflowY="auto">
      <Table {...styles.table}>
        <Thead>
          <Tr>
            <Th>Performer</Th>
          </Tr>
        </Thead>
        <Tbody>
          {performers?.map((performer, index) => (
            <Tr
              key={index}
              {...(selectedPerformerId === performer?.id &&
                styles.selectedStyle)}
            >
              <Td
                cursor={"pointer"}
                onClick={() => setSelectedPerformerId(performer.id)}
              >
                {performer.name}
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </TableContainer>
  );
};
export default PerformersTable;
