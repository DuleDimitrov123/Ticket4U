import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalCloseButton,
  Text,
  Box,
  ModalFooter,
} from "@chakra-ui/react";
import { Link } from "react-router-dom";

const NotSignInModal = ({ isOpen, onClose }) => {
  return (
    <Modal isOpen={isOpen} onClose={onClose} size={"md"}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Warning!</ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Box display="inline">
            <Text>You are not signed in!</Text>
            <Text>
              Please{" "}
              <Text
                color="purple.500"
                as={Link}
                to="/login"
                textDecoration="underline"
              >
                Sign in
              </Text>{" "}
              if you already has an account or{" "}
              <Text
                color="purple.500"
                as={Link}
                to="/signup"
                textDecoration="underline"
              >
                Sign Up
              </Text>{" "}
            </Text>
          </Box>
        </ModalBody>
        <ModalFooter></ModalFooter>
      </ModalContent>
    </Modal>
  );
};

export default NotSignInModal;
