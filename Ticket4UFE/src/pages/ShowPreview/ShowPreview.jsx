import { useParams } from "react-router-dom";
import useShows from "../../hooks/useShows";
import usePerformers from "../../hooks/usePerformers";
import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import { Flex } from "@chakra-ui/react";
import useCategories from "../../hooks/useCategories";

const ShowPreview = () => {
  const { showId } = useParams();
  const { showData, showLoading, refetchShow } = useShows(showId);
  const { performerData, performerLoading, refetchPerformer } = usePerformers(
    showData?.performerId
  );
  const { categoryData, categoryLoading, refetchCategory } = useCategories(
    showData?.categoryId
  );

  // Fetch the show details based on the showId and display them

  if (showLoading || performerLoading || categoryLoading) {
    return <div>Loading...</div>;
  }

  if (!showData) {
    return <div>Show not found</div>;
  }

  const {
    name,
    description,
    startingDateTime,
    tickerPriceAmount,
    ticketPriceCurrency,
  } = showData;

  return (
    <AuthenticatedLayout>
      <Flex flexWrap="wrap" h="100%" overflow={"auto"}>
        <h1>Show Preview - ID: {showId}</h1>
        <h2>{name}</h2>
        <p>{description}</p>
        <p>Starting Date and Time: {startingDateTime}</p>
        <p>
          Ticket Price: {tickerPriceAmount} {ticketPriceCurrency}
        </p>

        {/* You can use the rest of the showData properties as needed */}
      </Flex>
    </AuthenticatedLayout>
  );
};

export default ShowPreview;
