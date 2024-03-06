import { 
    Flex, 
    Heading, 
    Icon} from "@chakra-ui/react";
import useCategories from "../../hooks/useCategories";
import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import { BiLoader } from "react-icons/bi";
import styles from "./Categories.styles";
import CategoriesTable from "../../components/CategoryTable/CategoryTable";

const Categories = () => {
    const { categories, categoriesLoading } = useCategories();

    return (
        <AuthenticatedLayout>
          {categoriesLoading ? (
            <Flex w="100%" alignItems={"center"} justifyContent={"center"} h="100%">
              <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
            </Flex>
          ) : (
            <>
              {" "}
              <Flex {...styles.headerBox}>
                <Heading as="h3" size="lg" color="purple.500">
                  Categories
                </Heading>
              </Flex>
              <CategoriesTable
                categories={categories}
              />
            </>
          )}
        </AuthenticatedLayout>
    )
};
export default Categories;