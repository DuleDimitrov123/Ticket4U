import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import useShows from "../../hooks/useShows";
import {
  Button,
  Flex,
  Heading,
  Icon,
  Spacer,
  useDisclosure,
} from "@chakra-ui/react";
import EventCard from "../../components/EventCard";
import { BiLoader, BiPlus } from "react-icons/bi";
import styles from "./Shows.styles";
import { useContext, useState } from "react";
import { AuthContext } from "../../context/AuthContext";
import ShowsTable from "../../components/ShowsTable/ShowsTable";
import ShowModal from "../../components/ShowModal/ShowModal";
import PublicLayout from "../../layout/PublicLayout/PublicLayout";

const Shows = () => {
  const [isLoading, setIsLoading] = useState(false);
  const {
    showsData,
    showsLoading,
    refetchShows,
    deleteShow,
    createShow,
    updateShowName,
    updateShowLocation,
    updateShowPrice,
    updateShowDateTime,
  } = useShows("", setIsLoading);
  const { isAdmin, isAuthenticated } = useContext(AuthContext);
  const { isOpen, onOpen, onClose } = useDisclosure();

  if (!isAuthenticated()) {
    return (
      <PublicLayout>
        <Flex flexWrap="wrap" h="100%" overflow={"auto"} w="100%" py={2} px={4}>
          {showsLoading ? (
            <Flex
              w="100%"
              alignItems={"center"}
              justifyContent={"center"}
              h="100%"
            >
              <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
            </Flex>
          ) : (
            <>
              {showsData &&
                showsData.map((show, index) => (
                  <EventCard key={index} event={show} />
                ))}
            </>
          )}
        </Flex>
      </PublicLayout>
    );
  } else {
    return (
      <AuthenticatedLayout>
        {isAdmin() ? (
          <Flex flexWrap="wrap" h="100%" overflow={"auto"}>
            {showsLoading || isLoading ? (
              <Flex
                w="100%"
                alignItems={"center"}
                justifyContent={"center"}
                h="100%"
              >
                <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
              </Flex>
            ) : (
              <>
                <Flex {...styles.headerBox}>
                  <Heading as="h3" size="lg" color="purple.500">
                    Shows
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
                {showsData?.length && (
                  <ShowsTable
                    shows={showsData}
                    refetchShows={refetchShows}
                    createShow={createShow}
                    deleteShow={deleteShow}
                    updateShowName={updateShowName}
                    updateShowLocation={updateShowLocation}
                    updateShowPrice={updateShowPrice}
                    updateShowDateTime={updateShowDateTime}
                    isLoading={isLoading}
                    setIsLoading={setIsLoading}
                  />
                )}
                {isOpen && (
                  <ShowModal
                    isOpen={isOpen}
                    onClose={onClose}
                    createShow={createShow}
                  />
                )}
              </>
            )}
          </Flex>
        ) : (
          <Flex flexWrap="wrap" h="100%" overflow={"auto"}>
            {showsLoading ? (
              <Flex
                w="100%"
                alignItems={"center"}
                justifyContent={"center"}
                h="100%"
              >
                <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
              </Flex>
            ) : (
              <>
                <Flex {...styles.headerBox}>
                  <Heading as="h3" size="lg" color="purple.500">
                    Shows
                  </Heading>
                </Flex>
                {showsData &&
                  showsData.map((show, index) => (
                    <EventCard key={index} event={show} />
                  ))}
              </>
            )}
          </Flex>
        )}
      </AuthenticatedLayout>
    );
  }
};

export default Shows;
