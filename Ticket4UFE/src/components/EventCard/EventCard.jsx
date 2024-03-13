import React from "react";
import {
  Card,
  Heading,
  Text,
  CardBody,
  Stack,
  Divider,
  Image,
  CardFooter,
  ButtonGroup,
  Button,
  Flex,
  Icon,
} from "@chakra-ui/react";
import { BsCalendar } from "react-icons/bs";
import moment from "moment";
import styles from "./EventCard.styles";
import { useNavigate } from "react-router-dom";

const EventCard = ({ event }) => {
  const navigate = useNavigate();
  const {
    id,
    name,
    description,
    picture,
    startingDateTime,
    tickerPriceAmount,
    ticketPriceCurrency,
  } = event;
  const base64Image = `data:image/jpeg;base64,${picture}`;
  const formattedDateTime =
    moment(startingDateTime).format("DD/MM/YYYY, h:mm A");
  const handleViewDetails = () => {
    navigate(`/shows/${id}`);
  };

  return (
    <Card {...styles.eventCard} className="event-card">
      <CardBody>
        <Flex
          justifyContent="center"
          alignItems="center"
          height="250px"
          position="relative"
        >
          <Image
            src={base64Image}
            alt={name}
            borderRadius="lg"
            style={{ width: "auto", height: "100%" }}
            objectFit="cover"
          />
        </Flex>
        <Stack mt="6" spacing="3">
          <Heading size="md">{name}</Heading>
          <Text>{description}</Text>
          <Flex align="center">
            <Icon as={BsCalendar} boxSize={4} color="gray.500" />
            <Text ml={2} fontSize="sm" color="gray.500">
              {formattedDateTime}
            </Text>
          </Flex>
        </Stack>
        <Stack mt="2" spacing="1" align="start">
          <Text color="purple.600" fontSize="2xl">
            {tickerPriceAmount} {ticketPriceCurrency}
          </Text>
        </Stack>
      </CardBody>
      <Divider />
      <CardFooter>
        <ButtonGroup spacing="2">
          <Button
            variant="solid"
            colorScheme="purple"
            className="view_details"
            onClick={handleViewDetails}
          >
            View details
          </Button>
        </ButtonGroup>
      </CardFooter>
    </Card>
  );
};

export default EventCard;
