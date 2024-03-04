import { useMutation, useQuery } from "react-query";
import { publicFetch } from "../util/fetch";
import { useContext } from "react";
import { FetchContext } from "../context/FetchContext";

const useShows = (showId) => {
  const { protectedFetch } = useContext(FetchContext);
  const getShowsCallback = async () => {
    const { data } = await publicFetch.get("/shows");
    return data;
  };

  const getShowCallback = async () => {
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
  } = useQuery(["shows"], getShowsCallback);

  const {
    data: showData,
    isLoading: showLoading,
    refetch: refetchShow,
  } = useQuery(["show", showId], getShowCallback, {
    enabled: !!showId, // Enable the query only when showId is provided
  });

  const createShowCallback = async (data) => {
    const response = await protectedFetch.post("shows", data);

    return response.data;
  };

  const createShow = useMutation(createShowCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
  });

  return {
    showsData,
    showsLoading,
    refetchShows,
    showData,
    showLoading,
    refetchShow,
    createShow,
  };
};

export default useShows;
