import { useQuery } from "react-query";
import { useContext } from "react";
import { FetchContext } from "../context/FetchContext";

const usePerformers = (performerId) => {
  const { protectedFetch } = useContext(FetchContext);

  const getPerformerCallback = async () => {
    if (!performerId) {
      throw new Error("PerformerId is required");
    }

    const { data } = await protectedFetch.get(
      `/performers/${performerId}/detail`
    );
    return data;
  };

  const {
    data: performerData,
    isLoading: performerLoading,
    refetch: refetchPerformer,
  } = useQuery(["performer", performerId], getPerformerCallback, {
    enabled: !!performerId,
  });

  return {
    performerData,
    performerLoading,
    refetchPerformer,
  };
};

export default usePerformers;
