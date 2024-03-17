import { Flex, Heading, Icon } from "@chakra-ui/react";
import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import ReservationsTable from "../../components/ReservationTable/ReservationTable";
import styles from "./Reservations.styles";
import useReservation from "../../hooks/useReservation";
import { BiLoader } from "react-icons/bi";

const Reservations = () => {
  const userInfo = localStorage.getItem("userInfo");
  const userInfoJSON = JSON.parse(userInfo);
  const { reservationsData, reservationLoading, refetchReservations } =
    useReservation(userInfoJSON?.uid);

  return (
    <AuthenticatedLayout>
      {reservationLoading ? (
        <Flex w="100%" alignItems={"center"} justifyContent={"center"} h="100%">
          <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
        </Flex>
      ) : (
        <>
          {" "}
          <Flex {...styles.headerBox}>
            <Heading as="h3" size="lg" color="purple.500">
              My Reservations
            </Heading>
          </Flex>
          <ReservationsTable
            reservations={reservationsData}
            refetchReservations={refetchReservations}
          />
        </>
      )}
    </AuthenticatedLayout>
  );
};
export default Reservations;
