import {
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  Input,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalFooter,
  ModalHeader,
  ModalOverlay,
  Text,
  Textarea,
} from "@chakra-ui/react";
import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from "yup";
import useCategories from "../../hooks/useCategories";
import { useNavigate } from "react-router";

const AddCategoryModal = ({ isOpen, onClose, isEditFlow, category }) => {
  const { createCategory, refetchCategories, updateCategory } = useCategories();
  const initialValues = {
    categoryName: category?.name || "",
    categoryDescription: category?.description || "",
  };

  const validationSchema = Yup.object().shape({
    categoryName: Yup.string().required("This field is required"),
    categoryDescription: Yup.string().required("This field is required"),
  });

  const navigate = useNavigate();
  const onSubmit = async (values, { setSubmitting }) => {
    if (isEditFlow) {
      try {
        await updateCategory.mutateAsync(
          {
            categoryId: category.id,
            newCategoryName: values.categoryName,
            newCategoryDescription: values.categoryDescription,
          },
          {
            onSuccess: () => {
              refetchCategories();
              onClose();
            },
          }
        );
      } catch (error) {
        console.error("Error editing category: ", error);
      } finally {
        setSubmitting(false);
      }
    } else {
      try {
        await createCategory.mutateAsync(
          {
            categoryName: values.categoryName,
            categoryDescription: values.categoryDescription,
          },
          {
            onSuccess: (data) => {
              if (data) {
                refetchCategories();
              }
            },
          }
        );

        onClose();
      } catch (error) {
        console.error("Error creating category:", error);
      } finally {
        setSubmiting(false);
      }
    }
  };

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <Text as="span" fontWeight="bold">
            {isEditFlow ? "Edit category" : "Create category"}
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
                    errors.categoryName && touched.categoryName
                  )}
                >
                  <FormLabel htmlFor="categoryName">Category name</FormLabel>
                  <Field
                    as={Input}
                    type="text"
                    id="categoryName"
                    name="categoryName"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="categoryName" />
                  </FormErrorMessage>
                  <FormLabel htmlFor="categoryDescription">
                    Category description
                  </FormLabel>
                  <Field
                    as={Textarea}
                    id="categoryDescription"
                    name="categoryDescription"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="categoryDescription" />
                  </FormErrorMessage>
                </FormControl>
                <ModalFooter>
                  <Button
                    colorScheme="purple"
                    mr={3}
                    type="submit"
                    isLoading={isSubmitting}
                  >
                    {isEditFlow ? "Save changes" : "Create"}
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
          <Text as="span" fontWeight="bold"></Text>
        </ModalBody>
      </ModalContent>
    </Modal>
  );
};

export default AddCategoryModal;
