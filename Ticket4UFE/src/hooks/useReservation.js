import { useMutation, useQuery } from "react-query";
import { useContext } from "react";
import { FetchContext } from "../context/FetchContext";

const useReservation = (userId) => {
  const { protectedFetch } = useContext(FetchContext);

  const createReservationCallback = async ({
    externalUserId,
    externalShowId,
    numberOfTickets,
  }) => {
    const response = await protectedFetch.post("reservations", {
      externalUserId,
      externalShowId,
      numberOfReservations: numberOfTickets,
    });

    return response.data;
  };

  const updateReservationCallback = async ({
    reservationId,
    numberOfTickets,
  }) => {
    const response = await protectedFetch.put(
      `reservations/${reservationId}/newNumberOfResevations`,
      {
        newNumberOfReservations: numberOfTickets,
      }
    );

    return response.data;
  };

  const createReservation = useMutation(createReservationCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
  });

  const updateReservation = useMutation(updateReservationCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
  });

  const getReservationsByUserCallback = async () => {
    if (!userId) {
      throw new Error("userId is required");
    }

    const { data } = await protectedFetch.get(
      `/reservations/externalUser/${userId}`
    );
    return data;
  };

  const {
    data: reservationsData,
    isLoading: reservationLoading,
    refetch: refetchReservations,
  } = useQuery(["reservations", userId], getReservationsByUserCallback, {
    enabled: !!userId,
  });

  return {
    createReservation,
    updateReservation,
    reservationsData,
    reservationLoading,
    refetchReservations,
  };
};

export default useReservation;
