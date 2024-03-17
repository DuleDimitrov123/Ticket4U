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
  Box,
  Flex,
  Icon,
} from "@chakra-ui/react";
import { useState } from "react";
import { RiDeleteBin5Line } from "react-icons/ri";
import { AiOutlinePlus } from "react-icons/ai";
import usePerformers from "../../hooks/usePerformers";

const convertObjectToArray = (performerInfos) => {
  if (performerInfos) {
    return performerInfos.map((info) => ({
      name: info.name,
      value: info.value,
    }));
  }
};

const PerformerModal = ({ isOpen, onClose, isEditFlow, performer }) => {
  const initialValues = {
    name: performer?.name || "",
    performerInfoRequests:
      convertObjectToArray(performer?.performerInfos) || [],
  };
  const [performerInfoNumber, setPerformerInfoNumber] = useState(
    initialValues.performerInfoRequests.length || 1
  );
  const [performerInfoNamesToDelete, setPerformerInfoNamesToDelete] = useState(
    []
  );
  const { createPerformer, updatePerformerInfo, deletePerformerInfo } =
    usePerformers(performer?.id);

  const validationSchema = Yup.object().shape({
    name: Yup.string().required("This field is required"),
    performerInfoRequests: Yup.array()
      .of(
        Yup.object().shape({
          name: Yup.string().required("Info name is required"),
          value: Yup.string().required("Info value is required"),
        })
      )
      .min(1, "At least one performer info is required"),
  });

  const handleAddRow = () => {
    setPerformerInfoNumber((prev) => prev + 1);
  };

  const handleDeleteRow = (indexToDelete, values, setFieldValue) => {
    const name = values.performerInfoRequests[indexToDelete]?.name;
    const updatedRows = [
      ...values.performerInfoRequests.slice(0, indexToDelete),
      ...values.performerInfoRequests.slice(indexToDelete + 1),
    ];
    setPerformerInfoNamesToDelete([...performerInfoNamesToDelete, name]);
    setPerformerInfoNumber((prev) => prev - 1);
    setFieldValue("performerInfoRequests", updatedRows);
  };

  const handleInputChange = (index, updatedValue, values, setFieldValue) => {
    const updatedRows = [...values.performerInfoRequests];
    updatedRows[index] = { ...updatedRows[index], ...updatedValue };
    setFieldValue("performerInfoRequests", updatedRows);
  };

  const onSubmit = async (values, { setSubmitting }) => {
    const performerInfoObject = values.performerInfoRequests.reduce(
      (acc, info) => {
        acc[info.name] = info.value;
        return acc;
      },
      {}
    );
    const data = {
      name: values.name,
      performerId: performer?.id,
      performerInfoRequests: performerInfoObject,
    };
    if (isEditFlow) {
      try {
        await updatePerformerInfo.mutateAsync(data, {
          onSuccess: () => {
            onDelete();
          },
        });
      } catch (error) {
        console.error("Error updating performer:", error);
      } finally {
        setSubmitting(false);
      }
      return;
    }
    try {
      await createPerformer.mutateAsync(data, {
        onSuccess: () => {
          onClose();
        },
      });
    } catch (error) {
      console.error("Error creating performer:", error);
    } finally {
      setSubmitting(false);
    }
  };

  const onDelete = async () => {
    const data = {
      performerId: performer?.id,
      performerInfoNamesToDelete: performerInfoNamesToDelete,
    };
    try {
      await deletePerformerInfo.mutateAsync(data, {
        onSuccess: () => {
          onClose();
        },
      });
    } catch (error) {
      console.error("Error deleting performer:", error);
    }
    return;
  };
  return (
    <Modal isOpen={isOpen} onClose={onClose} size={"lg"}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          {isEditFlow ? "Edit performer" : "Add performer"}
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={onSubmit}
          >
            {({ isSubmitting, errors, touched, values, setFieldValue }) => {
              return (
                <Form>
                  <FormControl isInvalid={Boolean(errors.name && touched.name)}>
                    <FormLabel htmlFor="name">Name:</FormLabel>
                    <Field
                      as={Input}
                      type="text"
                      id="name"
                      name="name"
                      isDisabled={isEditFlow}
                    />
                    <FormErrorMessage>
                      <ErrorMessage name="name" />
                    </FormErrorMessage>
                  </FormControl>

                  <FormControl
                    isInvalid={Boolean(
                      errors.performerInfoRequests &&
                        touched.performerInfoRequests
                    )}
                  >
                    <FormLabel htmlFor="performerInfoRequests">
                      Performer info:
                    </FormLabel>
                    <Box display={"flex"} gap="3" flexDir={"column"}>
                      {Array.from({ length: performerInfoNumber }).map(
                        (value, index) => {
                          return (
                            <Flex key={index} align="center">
                              <Box mr={2}>{index + 1}.</Box>
                              <Flex gap="2">
                                <Input
                                  id={`info-name-${index}`}
                                  onChange={(e) =>
                                    handleInputChange(
                                      index,
                                      { name: e.target.value },
                                      values,
                                      setFieldValue
                                    )
                                  }
                                  placeholder="Info name"
                                  defaultValue={
                                    isEditFlow
                                      ? initialValues.performerInfoRequests[
                                          index
                                        ].name
                                      : ""
                                  }
                                  isDisabled={isEditFlow}
                                  width="100%"
                                />
                                <Input
                                  id={`info-value-${index}`}
                                  onChange={(e) =>
                                    handleInputChange(
                                      index,
                                      { value: e.target.value },
                                      values,
                                      setFieldValue
                                    )
                                  }
                                  placeholder="Info value"
                                  width="100%"
                                  defaultValue={
                                    isEditFlow
                                      ? initialValues.performerInfoRequests[
                                          index
                                        ].value
                                      : ""
                                  }
                                />
                              </Flex>
                              <Icon
                                as={RiDeleteBin5Line}
                                onClick={() =>
                                  handleDeleteRow(index, values, setFieldValue)
                                }
                                ml={2}
                              />
                              {index === performerInfoNumber - 1 &&
                                !isEditFlow && (
                                  <Button
                                    leftIcon={<AiOutlinePlus />}
                                    onClick={() => handleAddRow()}
                                    ml={2}
                                    height="40px"
                                    variant="outline"
                                  >
                                    Add
                                  </Button>
                                )}
                            </Flex>
                          );
                        }
                      )}
                    </Box>
                    <FormErrorMessage>
                      {Array.isArray(errors.performerInfoRequests) ? (
                        errors.performerInfoRequests.map((infoError, index) => (
                          <div key={index}>
                            {infoError?.name || infoError?.value}
                          </div>
                        ))
                      ) : (
                        <div>{errors.performerInfoRequests}</div>
                      )}
                    </FormErrorMessage>
                  </FormControl>
                  <ModalFooter>
                    <Button
                      colorScheme="purple"
                      mr={3}
                      type="submit"
                      isLoading={isSubmitting}
                    >
                      Save
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
              );
            }}
          </Formik>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export default PerformerModal;
