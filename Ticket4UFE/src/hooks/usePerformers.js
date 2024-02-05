import { useQuery } from "react-query";
import { useContext } from "react";
import { FetchContext } from "../context/FetchContext";

const usePerformers = (performerId) => {
  const { protectedFetch } = useContext(FetchContext);

  const getPerformers = async () => {
    const { data } = await protectedFetch.get("/performers");
    return data;
  };

  const getPerformer = async () => {
    if (!performerId) {
      throw new Error("PerformerId is required");
    }

    const { data } = await protectedFetch.get(
      `/performers/${performerId}/detail`
    );
    return data;
  };

  //   const {
  //     data: performersData,
  //     isLoading: performersLoading,
  //     refetch: refetchPerformers,
  //   } = useQuery(["performers"], getPerformers, {
  //     enabled: !performerId, // Only enable if performerId is not provided
  //   });

  const {
    data: performerData,
    isLoading: performerLoading,
    refetch: refetchPerformer,
  } = useQuery(["performer", performerId], getPerformer, {
    enabled: !!performerId,
  });

  return {
    // performersData,
    // performersLoading,
    // refetchPerformers,
    performerData,
    performerLoading,
    refetchPerformer,
  };
};

export default usePerformers;
