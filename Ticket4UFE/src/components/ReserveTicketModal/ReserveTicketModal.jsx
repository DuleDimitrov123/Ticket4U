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
import useReservation from "../../hooks/useReservation";
import { useNavigate } from "react-router";

const ReserveTicketModal = ({
  isOpen,
  onClose,
  show,
  reservationId,
  isEditFlow,
  numberOfReservations,
  refetchReservations,
}) => {
  const { createReservation, updateReservation } = useReservation();
  const initialValues = {
    numberOfTickets: numberOfReservations || 1,
  };
  const userInfo = localStorage.getItem("userInfo");
  const userInfoJSON = JSON.parse(userInfo);
  const navigate = useNavigate();

  const validationSchema = Yup.object().shape({
    numberOfTickets: Yup.number()
      .typeError("Please enter a valid number")
      .required("This field is required")
      .integer("Please enter a whole number")
      .min(1, "Number must be at least 1")
      .max(5, "Number must be at most 5"),
  });
  const onSubmit = async (values, { setSubmitting }) => {
    if (isEditFlow) {
      try {
        await updateReservation.mutateAsync(
          {
            reservationId: reservationId,
            numberOfTickets: values.numberOfTickets,
          },
          {
            onSuccess: () => {
              refetchReservations();
            },
          }
        );
        onClose();
      } catch (error) {
        console.error("Error creating reservation:", error);
      } finally {
        setSubmitting(false);
      }
      return;
    }
    try {
      await createReservation.mutateAsync(
        {
          externalUserId: userInfoJSON?.uid,
          externalShowId: show?.id,
          numberOfTickets: values.numberOfTickets,
        },
        {
          onSuccess: (data) => {
            if (data) {
              navigate("/reservations");
            }
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
          {isEditFlow ? "Edit reservation for show" : "Reserve ticket for show"}{" "}
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
                    errors.numberOfTickets && touched.numberOfTickets
                  )}
                >
                  <FormLabel htmlFor="numberOfTickets">
                    Number of Tickets:
                  </FormLabel>
                  <Field
                    as={Input}
                    type="number"
                    id="numberOfTickets"
                    name="numberOfTickets"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="numberOfTickets" />
                  </FormErrorMessage>
                </FormControl>
                <ModalFooter>
                  <Button
                    colorScheme="blue"
                    mr={3}
                    type="submit"
                    isLoading={isSubmitting}
                  >
                    {isEditFlow ? "Save changes" : "Reserve"}
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

export default ReserveTicketModal;
