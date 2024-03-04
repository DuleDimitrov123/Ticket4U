import {
  TableContainer,
  Table,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
  useDisclosure,
  Image,
  Text,
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
} from "@chakra-ui/react";
import styles from "./ShowsTable.styles";
import moment from "moment";
import { useState } from "react";
import { BsThreeDotsVertical } from "react-icons/bs";

const ShowsTable = ({ shows }) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [selectedShow, setSelectedShow] = useState({});

  const editShow = (show) => {
    setSelectedShow(show);
    onOpen();
  };
  const deleteShow = (show) => {
    console.log("Delete");
  };

  return (
    <TableContainer>
      <Table {...styles.table}>
        <Thead>
          <Tr>
            <Th>Show</Th>
            <Th>Location</Th>
            <Th>Date and time</Th>
            <Th>Number of places</Th>
            <Th>Status</Th>
            <Th>Price</Th>
            <Th>Action</Th>
          </Tr>
        </Thead>
        <Tbody>
          {shows.map((show, index) => (
            <Tr key={index}>
              <Td>
                {" "}
                <Text fontWeight={"bold"}>{show.name}</Text>
                <Image
                  src={`data:image/jpeg;base64,${show.picture}`}
                  alt={name}
                  borderRadius="lg"
                  style={{ maxHeight: "100px", width: "auto" }}
                  objectFit="cover"
                />
              </Td>
              <Td>{show.location}</Td>
              <Td>
                {moment(show.startingDateTime).format("DD/MM/YYYY, h:mm A")}
              </Td>
              <Td>{show.numberOfplaces}</Td>
              <Td>{show.status}</Td>
              <Td>
                {show.tickerPriceAmount} {show.ticketPriceCurrency}
              </Td>
              <Td>
                <Menu>
                  <MenuButton>
                    <BsThreeDotsVertical />
                  </MenuButton>
                  <MenuList>
                    <MenuItem>
                      {" "}
                      <Text color={"gray.800"} onClick={() => editShow(show)}>
                        Edit
                      </Text>
                    </MenuItem>
                    <MenuItem>
                      <Text color={"gray.800"} onClick={() => deleteShow(show)}>
                        Delete
                      </Text>
                    </MenuItem>
                  </MenuList>
                </Menu>
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
    </TableContainer>
  );
};

export default ShowsTable;
