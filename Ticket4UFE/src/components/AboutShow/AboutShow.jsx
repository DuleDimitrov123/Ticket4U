import { Text, Stack, ButtonGroup, Button, Flex, Icon } from "@chakra-ui/react";
import { BsCalendar } from "react-icons/bs";
import moment from "moment";
import { useNavigate } from "react-router-dom";
import styles from "./AboutShow.styles";
import { BiCreditCard, BiCurrentLocation } from "react-icons/bi";
import { MdEventSeat } from "react-icons/md";

const AboutShow = ({ show, handleBuyTicket, openModal }) => {
  const navigate = useNavigate();
  const {
    id,
    description,
    location,
    startingDateTime,
    tickerPriceAmount,
    ticketPriceCurrency,
    numberOfplaces,
    status,
  } = show;
  const formattedDateTime =
    moment(startingDateTime).format("DD/MM/YYYY, h:mm A");

  return (
    <Flex {...styles.aboutShowCard}>
      <Stack mt="6" spacing="3">
        <Text fontSize="sm" color="gray.500" fontStyle={"italic"}>
          {description}
        </Text>
        <Flex align="center">
          <Icon as={BiCurrentLocation} boxSize={4} color="purple.500" />
          <Text ml={2} fontSize="sm" color="black" fontWeight={"500"}>
            Location:
          </Text>
          <Text ml={2} fontSize="sm" color="gray.500">
            {location}
          </Text>
        </Flex>
        <Flex align="center">
          <Icon as={BsCalendar} boxSize={4} color="purple.500" />
          <Text ml={2} fontSize="sm" color="black" fontWeight={"500"}>
            Date and time:
          </Text>
          <Text ml={2} fontSize="sm" color="gray.500">
            {formattedDateTime}
          </Text>
        </Flex>
        <Flex align="center">
          <Icon as={MdEventSeat} boxSize={4} color="purple.500" />
          <Text ml={2} fontSize="sm" color="black" fontWeight={"500"}>
            Number of places:
          </Text>
          <Text ml={2} fontSize="sm" color="gray.500">
            {numberOfplaces}
          </Text>
        </Flex>
        <Flex align="center">
          <Icon as={BiCreditCard} boxSize={4} color="purple.500" />
          <Text ml={2} fontSize="sm" color="black" fontWeight={"500"}>
            Price:
          </Text>
          <Text ml={2} fontSize="sm" color="gray.500">
            {tickerPriceAmount} {ticketPriceCurrency}
          </Text>
        </Flex>
      </Stack>
      <Flex spacing="2" mt={"4"} flexDirection={"column"} gap="1">
        <Button
          variant="solid"
          colorScheme="purple"
          className="view_details"
          onClick={openModal}
          isDisabled={status === "IsSoldOut"}
          width={"30%"}
        >
          Reserve
        </Button>
        {status === "IsSoldOut" && (
          <Text size="sm" color={"red.500"}>
            This show is sold out!
          </Text>
        )}
      </Flex>
    </Flex>
  );
};

export default AboutShow;
