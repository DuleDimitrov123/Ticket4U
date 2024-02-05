import { useQuery } from "react-query";
import { useContext } from "react";
import { FetchContext } from "../context/FetchContext";

const useCategories = (categoryId) => {
  const { protectedFetch } = useContext(FetchContext);

  const getCategory = async () => {
    if (!categoryId) {
      throw new Error("CategoryId is required");
    }

    const { data } = await protectedFetch.get(`/categories/${categoryId}`);
    return data;
  };

  const {
    data: categoryData,
    isLoading: categoryLoading,
    refetch: refetchCategory,
  } = useQuery(["category", categoryId], getCategory, {
    enabled: !!categoryId,
  });

  return {
    categoryData,
    categoryLoading,
    refetchCategory,
  };
};

export default useCategories;
