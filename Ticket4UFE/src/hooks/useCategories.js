import { useMutation, useQuery } from "react-query";
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

  const createCategoryCallback = async ({
    categoryName,
    categoryDescription
  }) => {
    const response = await protectedFetch.post("categories", {
      name: categoryName,
      description: categoryDescription
    });

    return response.data;
  }

  const updateCategoryCallback = async ({
    categoryId,
    newCategoryName,
    newCategoryDescription
  }) => {
    const response = await protectedFetch.put(
      `categories/${categoryId}`,
      {
        newName: newCategoryName,
        newDescription: newCategoryDescription
      }
    );

    return response.data;
  }

  const deleteCategoryCallback = async ({categoryId}) => {
    const response = await protectedFetch.put(`categories/${categoryId}/archive`);
    return response.data;
  }

  const createCategory = useMutation(createCategoryCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
  })

  const updateCategory = useMutation(updateCategoryCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    }
  })

  const deleteCategory = useMutation(deleteCategoryCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    }
  })

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
    createCategory,
    updateCategory,
    deleteCategory,
    categoryData,
    categoryLoading,
    refetchCategory,
    categories,
    categoriesLoading,
    refetchCategories,
  };
};

export default useCategories;
