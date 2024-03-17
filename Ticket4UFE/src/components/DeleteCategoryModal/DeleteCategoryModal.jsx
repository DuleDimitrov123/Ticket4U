import {
  Button,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Text,
} from "@chakra-ui/react";

const DeleteCategoryModal = ({ isOpen, onClose, category, onDelete }) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          {"Delete category"}
          <Text as="span" fontWeight="bold"></Text>
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Text>Are you sure you want to delete category {category?.name}</Text>
        </ModalBody>
        <ModalFooter>
          <Button
            colorScheme="purple"
            mr={3}
            onClick={() => onDelete(category)}
          >
            Delete
          </Button>
          <Button variant="ghost" type="button" onClick={onClose}>
            Cancel
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default DeleteCategoryModal;
