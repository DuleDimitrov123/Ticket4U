import { 
  Button,
    Flex, 
    Heading, 
    Icon,
    Spacer,
    useDisclosure
} from "@chakra-ui/react";
import useCategories from "../../hooks/useCategories";
import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import { BiLoader, BiPlus } from "react-icons/bi";
import styles from "./Categories.styles";
import CategoriesTable from "../../components/CategoryTable/CategoryTable";
import AddCategoryModal from "../../components/AddCategoryModal";

const Categories = () => {
    const { categories, categoriesLoading } = useCategories();
    const { isOpen, onOpen, onClose } = useDisclosure();
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
                <Spacer />
                <Button
                  variant="solid"
                  colorScheme="purple"
                  className="view_details"
                  onClick={onOpen}
                  width={"130px"}
                  leftIcon={BiPlus}
                >
                  Add Category
                </Button>
              </Flex>
              <CategoriesTable
                categories={categories}
              />
              <AddCategoryModal 
                isOpen={isOpen}
                onClose={onClose}
              />
            </>
          )}
        </AuthenticatedLayout>
    )
};
export default Categories;