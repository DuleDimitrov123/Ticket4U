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
import styles from "./ReservationTable.styles";
import moment from "moment";
import ReserveTicketModal from "../ReserveTicketModal";
import { useState } from "react";

const ReservationsTable = ({ reservations, refetchReservations }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [selectedReservation, setSelectedReservation] = useState({});

  const editReservation = (reservation) => {
    setSelectedReservation(reservation);
    onOpen();
  };
  return (
    <TableContainer>
      <Table {...styles.table}>
        <Thead>
          <Tr>
            <Th>Show name</Th>
            <Th>Date and time</Th>
            <Th>Num of places</Th>
            <Th>Is sold out</Th>
            <Th>Edit</Th>
          </Tr>
        </Thead>
        <Tbody>
          {reservations?.map((reservation, index) => (
            <Tr key={index}>
              <Td>{reservation.show.showName}</Td>
              <Td>
                {moment(reservation.show.startingDateTime).format(
                  "DD/MM/YYYY, h:mm A"
                )}
              </Td>
              <Td>{reservation.numberOfReservations}</Td>
              <Td>
                {reservation.show.isSoldOut ? "SOLD OUT" : "NOT SOLD OUT"}
              </Td>
              <Td>
                <Button
                  variant={"outline"}
                  color={"purple.500"}
                  borderColor={"gray.400"}
                  disabled={reservation.show.isSoldOut}
                  onClick={() => editReservation(reservation)}
                >
                  Edit
                </Button>
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
      <ReserveTicketModal
        isOpen={isOpen}
        onClose={onClose}
        show={selectedReservation?.show}
        reservationId={selectedReservation?.id}
        isEditFlow
        numberOfReservations={selectedReservation?.numberOfReservations}
        refetchReservations={refetchReservations}
      />
    </TableContainer>
  );
};
export default ReservationsTable;
