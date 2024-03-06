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
    onSettled: () => {
      refetchShows();
    },
  });

  const deleteShowCallback = async ({ showId }) => {
    const response = await protectedFetch.delete(`/shows/${showId}`);
    return response.data;
  };

  const deleteShow = useMutation(deleteShowCallback, {
    onSettled: () => {
      refetchShows();
    },
  });

  const updateShowNameCallback = async ({ showId, newName }) => {
    const response = await protectedFetch.put(`/shows/${showId}/newName`, {
      newName,
    });
    return response.data;
  };

  const updateShowName = useMutation(updateShowNameCallback, {
    onSettled: () => {
      refetchShows();
    },
  });

  const updateShowLocationCallback = async ({ showId, newLocation }) => {
    const response = await protectedFetch.put(`/shows/${showId}/newLocation`, {
      newLocation,
    });
    return response.data;
  };

  const updateShowLocation = useMutation(updateShowLocationCallback, {
    onSettled: () => {
      refetchShows();
    },
  });

  const updateShowPriceCallback = async ({ showId, newAmount }) => {
    const response = await protectedFetch.put(`/shows/${showId}/newPrice`, {
      newAmount,
    });
    return response.data;
  };

  const updateShowPrice = useMutation(updateShowPriceCallback, {
    onSettled: () => {
      refetchShows();
    },
  });

  const updateShowDateTimeCallback = async ({
    showId,
    newStartingDateTime,
  }) => {
    const response = await protectedFetch.put(
      `/shows/${showId}/newStartingDateTime`,
      {
        newStartingDateTime,
      }
    );
    return response.data;
  };

  const updateShowDateTime = useMutation(updateShowDateTimeCallback, {
    onSettled: () => {
      refetchShows();
    },
  });

  const createShowMessageCallback = async ({
    showId,
    showMessageName,
    showMessageValue,
  }) => {
    const response = await protectedFetch.post(
      `/shows/${showId}/showMessages`,
      {
        showMessageName,
        showMessageValue,
      }
    );
    return response.data;
  };

  const createShowMessage = useMutation(createShowMessageCallback, {
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
    deleteShow,
    updateShowName,
    updateShowLocation,
    updateShowPrice,
    updateShowDateTime,
    createShowMessage,
  };
};

export default useShows;
