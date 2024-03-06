import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  ModalCloseButton,
  Button,
  Input,
  FormControl,
  FormLabel,
  FormErrorMessage,
  Text,
} from "@chakra-ui/react";
import useShows from "../../hooks/useShows";

const AddShowMessageModal = ({ isOpen, onClose, show, refetchShow }) => {
  const { createShowMessage } = useShows();
  const initialValues = {
    showMessageName: "",
    showMessageValue: "",
  };

  const validationSchema = Yup.object().shape({
    showMessageName: Yup.string().required("This field is required"),
    showMessageValue: Yup.string().required("This field is required"),
  });
  const onSubmit = async (values, { setSubmitting }) => {
    try {
      await createShowMessage.mutateAsync(
        {
          ...values,
          showId: show?.id,
        },
        {
          onSuccess: () => {
            refetchShow();
          },
        }
      );

      onClose();
    } catch (error) {
      console.error("Error creating reservation:", error);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          Add message for show
          <Text as="span" fontWeight="bold">
            {show?.name}
          </Text>
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={onSubmit}
          >
            {({ isSubmitting, errors, touched }) => (
              <Form>
                <FormControl
                  isInvalid={Boolean(
                    errors.showMessageName && touched.showMessageName
                  )}
                >
                  <FormLabel htmlFor="showMessageName">Message name:</FormLabel>
                  <Field
                    as={Input}
                    type="string"
                    id="showMessageName"
                    name="showMessageName"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="showMessageName" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(
                    errors.showMessageValue && touched.showMessageValue
                  )}
                >
                  <FormLabel htmlFor="showMessageValue">
                    Message value:
                  </FormLabel>
                  <Field
                    as={Input}
                    type="string"
                    id="showMessageValue"
                    name="showMessageValue"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="showMessageValue" />
                  </FormErrorMessage>
                </FormControl>
                <ModalFooter>
                  <Button
                    colorScheme="blue"
                    mr={3}
                    type="submit"
                    isLoading={isSubmitting}
                  >
                    {"Save message"}
                  </Button>
                  <Button
                    variant="ghost"
                    type="button"
                    onClick={onClose}
                    isDisabled={isSubmitting}
                  >
                    Cancel
                  </Button>
                </ModalFooter>
              </Form>
            )}
          </Formik>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export default AddShowMessageModal;
