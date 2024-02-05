import { useQuery } from "react-query";
import { publicFetch } from "../util/fetch";

const useShows = (showId) => {
  const getShows = async () => {
    const { data } = await publicFetch.get("/shows");
    return data;
  };

  const getShow = async () => {
    if (!showId) {
      throw new Error("showId is required");
    }

    const { data } = await publicFetch.get(`/shows/${showId}/detail`);
    return data;
  };

  const {
    data: showsData,
    isLoading: showsLoading,
    refetch: refetchShows,
  } = useQuery(["shows"], getShows);

  const {
    data: showData,
    isLoading: showLoading,
    refetch: refetchShow,
  } = useQuery(["show", showId], getShow, {
    enabled: !!showId, // Enable the query only when showId is provided
  });

  return {
    showsData,
    showsLoading,
    refetchShows,
    showData,
    showLoading,
    refetchShow,
  };
};

export default useShows;
