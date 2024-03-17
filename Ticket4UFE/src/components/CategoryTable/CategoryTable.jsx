import {
  TableContainer,
  Table,
  Tbody,
  Td,
  Th,
  Thead,
  Tr,
  Menu,
  MenuButton,
  MenuList,
  MenuItem,
  Text,
  useDisclosure,
} from "@chakra-ui/react";
import styles from "./CategoryTable.styles";
import { BsThreeDotsVertical } from "react-icons/bs";
import { useState } from "react";
import AddCategoryModal from "../AddCategoryModal/AddCategoryModal";
import DeleteCategoryModal from "../DeleteCategoryModal";
import useCategories from "../../hooks/useCategories";

const CategoriesTable = ({ categories }) => {
  const { deleteCategory, refetchCategories } = useCategories();
  const { isOpen, onOpen, onClose } = useDisclosure();
  const {
    isOpen: isDeleteModalOpen,
    onOpen: onDeleteModalOpen,
    onClose: onDeleteModalClose,
  } = useDisclosure();

  const [selectedCategory, setSelectedCategory] = useState({});

  const editCategory = (category) => {
    setSelectedCategory(category);
    onOpen();
  };

  const handleDeleteCategory = (category) => {
    setSelectedCategory(category);
    onDeleteModalOpen();
  };

  const onDelete = async (category) => {
    try {
      await deleteCategory.mutateAsync({
        categoryId: category.id,
      }),
        {
          onSuccess: () => {},
        };
      onDeleteModalClose();
    } catch (error) {
      console.error("Error deleting category:", error);
    }
  };

  return (
    <TableContainer>
      <Table {...styles.table}>
        <Thead>
          <Tr>
            <Th>Category name</Th>
            <Th>Category description</Th>
            <Th>Status</Th>
            <Th>Action</Th>
          </Tr>
        </Thead>
        <Tbody>
          {categories?.map((category, index) => (
            <Tr
              key={index}
              opacity={category.status === "IsArchived" ? 0.5 : 1}
            >
              <Td>{category.name}</Td>
              <Td>{category.description}</Td>
              <Td>
                {category.status === "IsArchived" ? "Archived" : "Active"}
              </Td>
              <Td>
                {category.status !== "IsArchived" && (
                  <Menu>
                    <MenuButton>
                      <BsThreeDotsVertical />
                    </MenuButton>
                    <MenuList>
                      <MenuItem onClick={() => editCategory(category)}>
                        {" "}
                        <Text color={"gray.800"}>Edit</Text>
                      </MenuItem>
                      <MenuItem onClick={() => handleDeleteCategory(category)}>
                        <Text color={"gray.800"}>Delete</Text>
                      </MenuItem>
                    </MenuList>
                  </Menu>
                )}
              </Td>
            </Tr>
          ))}
        </Tbody>
      </Table>
      <DeleteCategoryModal
        isOpen={isDeleteModalOpen}
        onClose={onDeleteModalClose}
        category={selectedCategory}
        onDelete={onDelete}
      ></DeleteCategoryModal>

      <AddCategoryModal
        isOpen={isOpen}
        onClose={onClose}
        isEditFlow
        category={selectedCategory}
      />
    </TableContainer>
  );
};
export default CategoriesTable;
