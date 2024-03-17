import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  Button,
  Text,
} from "@chakra-ui/react";

const DeleteShowModal = ({ isOpen, onClose, show, onDelete }) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          {"Delete show"}
          <Text as="span" fontWeight="bold"></Text>
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Text>Are you sure you want to delete show {show?.name}</Text>
        </ModalBody>
        <ModalFooter>
          <Button colorScheme="purple" mr={3} onClick={() => onDelete(show)}>
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

export default DeleteShowModal;
