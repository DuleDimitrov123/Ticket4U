import { useMutation, useQuery } from "react-query";
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

  const getPerformersCallback = async () => {
    const { data } = await protectedFetch.get(`/performers`);
    return data;
  };

  const {
    data: performerData,
    isLoading: performerLoading,
    refetch: refetchPerformer,
  } = useQuery(["performer", performerId], getPerformerCallback, {
    enabled: !!performerId,
  });

  const {
    data: performers,
    isLoading: performersLoading,
    refetch: refetchPerformers,
  } = useQuery(["performers", performerId], getPerformersCallback);

  const createPerformerCallback = async (data) => {
    const response = await protectedFetch.post("performers", data);

    return response.data;
  };

  const createPerformer = useMutation(createPerformerCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
    onSettled: () => {
      refetchPerformers();
    },
  });

  const updatePerformerInfoCallback = async ({
    performerId,
    performerInfoRequests,
  }) => {
    const response = await protectedFetch.put(
      `/performers/${performerId}/performer-info`,
      { performerInfoRequests: performerInfoRequests }
    );
    return response.data;
  };

  const updatePerformerInfo = useMutation(updatePerformerInfoCallback, {
    onSettled: () => {
      refetchPerformer();
    },
  });

  const deletePerformerInfoCallback = async ({
    performerId,
    performerInfoNamesToDelete,
  }) => {
    console.log("performerInfoNamesToDelete", performerInfoNamesToDelete);
    const response = await protectedFetch.delete(
      `/performers/${performerId}/performer-info`,
      {
        headers: {
          "Content-Type": "application/json", // Set the Content-Type header
        },
        data: JSON.stringify({
          performerInfoNamesToDelete,
        }),
      }
    );
    return response.data;
  };

  const deletePerformerInfo = useMutation(deletePerformerInfoCallback, {
    onSettled: () => {
      refetchPerformer();
    },
  });
  return {
    performerData,
    performerLoading,
    refetchPerformer,
    performers,
    performersLoading,
    refetchPerformers,
    createPerformer,
    updatePerformerInfo,
    deletePerformerInfo,
  };
};

export default usePerformers;
