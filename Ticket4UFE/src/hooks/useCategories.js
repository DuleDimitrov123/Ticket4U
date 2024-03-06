import { useQuery } from "react-query";
import { useContext } from "react";
import { FetchContext } from "../context/FetchContext";

const useCategories = (categoryId) => {
  const { protectedFetch } = useContext(FetchContext);

  const getCategoryCallback = async () => {
    if (!categoryId) {
      throw new Error("CategoryId is required");
    }

    const { data } = await protectedFetch.get(`/categories/${categoryId}`);
    return data;
  };

  const getCategoriesCallback = async () => {
    const { data } = await protectedFetch.get(`/categories`);
    return data;
  };

  const {
    data: categoryData,
    isLoading: categoryLoading,
    refetch: refetchCategory,
  } = useQuery(["category", categoryId], getCategoryCallback, {
    enabled: !!categoryId,
  });

  const {
    data: categories,
    isLoading: categoriesLoading,
    refetch: refetchCategories,
  } = useQuery(["categories"], getCategoriesCallback);

  return {
    categoryData,
    categoryLoading,
    refetchCategory,
    categories,
    categoriesLoading,
    refetchCategories,
  };
};

export default useCategories;
