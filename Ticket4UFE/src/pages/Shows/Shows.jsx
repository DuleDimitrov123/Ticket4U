import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import useShows from "../../hooks/useShows";
import { Flex, Icon } from "@chakra-ui/react";
import EventCard from "../../components/EventCard";
import { BiLoader } from "react-icons/bi";

const Shows = () => {
  const { showsData, showsLoading } = useShows();

  return (
    <AuthenticatedLayout>
      <Flex flexWrap="wrap" h="100%" overflow={"auto"}>
        {showsLoading && <Icon as={BiLoader} />}
        {!showsLoading &&
          showsData &&
          showsData.map((show, index) => (
            <EventCard key={index} event={show} />
          ))}
      </Flex>
    </AuthenticatedLayout>
  );
};

export default Shows;
