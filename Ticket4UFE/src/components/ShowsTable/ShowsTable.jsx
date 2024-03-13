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
import DeleteShowModal from "../DeleteShowModal/DeleteShowModal";
import ShowModal from "../ShowModal/ShowModal";
import { useNavigate } from "react-router";

const ShowsTable = ({
  shows,
  refetchShows,
  deleteShow,
  createShow,
  updateShowName,
  updateShowLocation,
  updateShowPrice,
  updateShowDateTime,
  setIsLoading,
}) => {
  const navigate = useNavigate();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [selectedShow, setSelectedShow] = useState({});

  const {
    isOpen: isDeleteModalOpen,
    onOpen: onDeleteModalOpen,
    onClose: onDeleteModalClose,
  } = useDisclosure();

  const editShow = (show) => {
    setSelectedShow(show);
    onOpen();
  };
  const handleDelete = (show) => {
    setSelectedShow(show);
    onDeleteModalOpen();
  };

  const onDelete = async (show) => {
    try {
      setIsLoading(true);

      await deleteShow.mutateAsync(
        {
          showId: show.id,
        },
        {
          onSuccess: () => {
            onDeleteModalClose();
          },
        }
      );
    } catch (error) {
      console.error("Error deleting show:", error);
    }
  };

  const handleViewDetails = (id) => {
    navigate(`/shows/${id}`);
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
              <Td
                cursor={"pointer"}
                onClick={() => handleViewDetails(show?.id)}
              >
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
                    <MenuItem onClick={() => editShow(show)}>
                      {" "}
                      <Text color={"gray.800"}>Edit</Text>
                    </MenuItem>
                    <MenuItem onClick={() => handleDelete(show)}>
                      <Text color={"gray.800"}>Delete</Text>
                    </MenuItem>
                  </MenuList>
                </Menu>
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
      <DeleteShowModal
        isOpen={isDeleteModalOpen}
        onClose={onDeleteModalClose}
        show={selectedShow}
        onDelete={onDelete}
      />

      <ShowModal
        isOpen={isOpen}
        onClose={onClose}
        show={selectedShow}
        isEditFlow
        refetchShows={refetchShows}
        createShow={createShow}
        updateShowName={updateShowName}
        updateShowLocation={updateShowLocation}
        updateShowPrice={updateShowPrice}
        updateShowDateTime={updateShowDateTime}
      />
    </TableContainer>
  );
};

export default ShowsTable;
