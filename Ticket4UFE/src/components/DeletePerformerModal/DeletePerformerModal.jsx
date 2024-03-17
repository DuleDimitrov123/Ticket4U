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

const DeletePerformerModal = ({ isOpen, onClose, performer, onDelete }) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          Delete
          <Text as="span" fontWeight="bold"></Text>
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Text>
            Are you sure you want to delete performer {performer?.name}
          </Text>
        </ModalBody>
        <ModalFooter>
          <Button
            colorScheme="purple"
            mr={3}
            onClick={() => onDelete(performer)}
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

export default DeletePerformerModal;
