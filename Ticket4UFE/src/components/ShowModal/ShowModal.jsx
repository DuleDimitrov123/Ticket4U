import { Formik, Field, Form, ErrorMessage } from "formik";
import * as Yup from "yup";
import {
  Drawer,
  DrawerBody,
  DrawerFooter,
  DrawerHeader,
  DrawerOverlay,
  DrawerContent,
  DrawerCloseButton,
  Input,
  Button,
  FormControl,
  FormLabel,
  FormErrorMessage,
  Select,
  Flex,
} from "@chakra-ui/react";
import { useRef, useState } from "react";
import ImageUpload from "../ImageUpload/ImageUpload";
import DateAndTimePicker from "../DateAndTimePicker/DateAndTimePicker";
import useCategories from "../../hooks/useCategories";
import usePerformers from "../../hooks/usePerformers";

const ShowModal = ({
  isOpen,
  onClose,
  isEditFlow,
  show,
  createShow,
  updateShowName,
  updateShowLocation,
  updateShowPrice,
  updateShowDateTime,
}) => {
  const btnRef = useRef();
  const initialValues = {
    name: show?.name || "",
    description: show?.description || "",
    picture: show?.picture || "",
    location: show?.location || "",
    numberOfplaces: show?.numberOfplaces || "",
    ticketPriceCurrency: show?.ticketPriceCurrency || "",
    tickerPriceAmount: show?.tickerPriceAmount || "",
    startingDateTime: show?.startingDateTime || "",
    categoryId: show?.categoryId || "",
    performerId: show?.performerId || "",
  };
  const { categories } = useCategories();
  const { performers } = usePerformers();
  const [isSubmittingName, setIsSubmittingName] = useState(false);
  const [isSubmittingLocation, setIsSubmittingLocation] = useState(false);
  const [isSubmittingPrice, setIsSubmittingPrice] = useState(false);
  const [isSubmittingDateTime, setIsSubmittingDateTime] = useState();

  const validationSchema = Yup.object().shape({
    name: Yup.string().required("This field is required"),
    description: Yup.string().required("This field is required"),
    location: Yup.string().required("This filed is required"),
    picture: Yup.string().required("This filed is required"),
    numberOfplaces: Yup.number()
      .integer("Please enter a valid integer")
      .min(1, "Number of places must be greater than 0")
      .required("This field is required"),
    ticketPriceCurrency: Yup.string().required("This filed is required"),
    tickerPriceAmount: Yup.number()
      .integer("Please enter a valid integer")
      .min(0, "Amount must positive integer")
      .required("This filed is required"),
    startingDateTime: "",
  });

  const onSubmit = async (values, { setSubmitting }) => {
    const data = {
      ...values,
      startingDateTime: new Date(values.startingDateTime).toISOString(),
    };
    setSubmitting(true);
    try {
      await createShow.mutateAsync(data, {
        onSuccess: () => {
          onClose();
        },
      });
    } catch (error) {
      console.error("Error creating reservation:", error);
    } finally {
      setSubmitting(false);
    }
  };

  const onUpdateName = async (name) => {
    try {
      setIsSubmittingName(true);
      await updateShowName.mutateAsync({
        showId: show.id,
        newName: name,
      });
    } catch (error) {
      console.error("Error updating name:", error);
    } finally {
      setIsSubmittingName(false);
    }
    return;
  };
  const onUpdateLocation = async (location) => {
    try {
      setIsSubmittingLocation(true);
      await updateShowLocation.mutateAsync({
        showId: show.id,
        newLocation: location,
      });
    } catch (error) {
      console.error("Error updating location:", error);
    } finally {
      setIsSubmittingLocation(false);
    }
    return;
  };

  const onUpdatePrice = async (price) => {
    try {
      setIsSubmittingPrice(true);
      await updateShowPrice.mutateAsync({
        showId: show.id,
        newAmount: price,
      });
    } catch (error) {
      console.error("Error updating price:", error);
    } finally {
      setIsSubmittingPrice(false);
    }
    return;
  };

  const onUpdateDateTime = async (dateTime) => {
    try {
      setIsSubmittingDateTime(true);
      await updateShowDateTime.mutateAsync({
        showId: show.id,
        newStartingDateTime: dateTime,
      });
    } catch (error) {
      console.error("Error updating price:", error);
    } finally {
      setIsSubmittingDateTime(false);
    }
    return;
  };

  return (
    <Drawer
      isOpen={isOpen}
      placement="right"
      onClose={onClose}
      finalFocusRef={btnRef}
    >
      <DrawerOverlay />
      <DrawerContent>
        <DrawerCloseButton />
        <DrawerHeader>{isEditFlow ? "Edit show" : "Create show"}</DrawerHeader>
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={onSubmit}
        >
          {({ errors, touched, values, setFieldValue }) => (
            <Form>
              <DrawerBody maxH="80vh" overflowY="auto">
                <FormControl isInvalid={Boolean(errors.name && touched.name)}>
                  <FormLabel htmlFor="name"> Name:</FormLabel>
                  {isEditFlow ? (
                    <Flex flexDir={"column"} gap="1" alignItems={"end"}>
                      <Field as={Input} type="string" id="name" name="name" />
                      <Button
                        colorScheme="purple"
                        onClick={() => onUpdateName(values?.name)}
                        isLoading={isSubmittingName}
                      >
                        Update name
                      </Button>
                    </Flex>
                  ) : (
                    <Field as={Input} type="string" id="name" name="name" />
                  )}

                  <FormErrorMessage>
                    <ErrorMessage name="name" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.description && touched.description)}
                >
                  <FormLabel htmlFor="description">Description:</FormLabel>
                  <Field
                    as={Input}
                    type="string"
                    id="description"
                    name="description"
                    disabled={isEditFlow}
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="description" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.picture && touched.picture)}
                >
                  <FormLabel htmlFor="picture">Image:</FormLabel>
                  <ImageUpload isEditFlow={isEditFlow} />
                  <FormErrorMessage>
                    <ErrorMessage name="picture" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.location && touched.location)}
                >
                  <FormLabel htmlFor="location">Location:</FormLabel>
                  {isEditFlow ? (
                    <Flex flexDir={"column"} gap="1" alignItems={"end"}>
                      <Field
                        as={Input}
                        type="string"
                        id="location"
                        name="location"
                      />
                      <Button
                        colorScheme="purple"
                        onClick={() => onUpdateLocation(values?.location)}
                        isLoading={isSubmittingLocation}
                      >
                        Update location
                      </Button>
                    </Flex>
                  ) : (
                    <Field
                      as={Input}
                      type="string"
                      id="location"
                      name="location"
                    />
                  )}

                  <FormErrorMessage>
                    <ErrorMessage name="location" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(
                    errors.numberOfplaces && touched.numberOfplaces
                  )}
                >
                  <FormLabel htmlFor="numberOfplaces">
                    Number of places:
                  </FormLabel>
                  <Field
                    as={Input}
                    type="number"
                    id="numberOfplaces"
                    name="numberOfplaces"
                    disabled={isEditFlow}
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="numberOfplaces" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(
                    errors.ticketPriceCurrency && touched.ticketPriceCurrency
                  )}
                >
                  <FormLabel htmlFor="ticketPriceCurrency">Currency:</FormLabel>
                  <Select
                    id="ticketPriceCurrency"
                    name="ticketPriceCurrency"
                    placeholder="Select currency"
                    value={values.ticketPriceCurrency}
                    onChange={(e) =>
                      setFieldValue("ticketPriceCurrency", e.target.value)
                    }
                    disabled={isEditFlow}
                  >
                    <option value="RSD">RSD</option>
                    <option value="EUR">EUR</option>
                  </Select>
                  <FormErrorMessage>
                    <ErrorMessage name="ticketPriceCurrency" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.location && touched.location)}
                >
                  <FormLabel htmlFor="tickerPriceAmount">
                    Ticket price:
                  </FormLabel>
                  {isEditFlow ? (
                    <Flex flexDir={"column"} gap="1" alignItems={"end"}>
                      <Field
                        as={Input}
                        type="number"
                        id="tickerPriceAmount"
                        name="tickerPriceAmount"
                      />
                      <Button
                        colorScheme="purple"
                        onClick={() => onUpdatePrice(values?.tickerPriceAmount)}
                        isLoading={isSubmittingPrice}
                      >
                        Update price
                      </Button>
                    </Flex>
                  ) : (
                    <Field
                      as={Input}
                      type="number"
                      id="tickerPriceAmount"
                      name="tickerPriceAmount"
                    />
                  )}

                  <FormErrorMessage>
                    <ErrorMessage name="tickerPriceAmount" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(
                    errors.startingDateTime && touched.startingDateTime
                  )}
                >
                  <FormLabel htmlFor="startingDateTime">
                    Date and time:
                  </FormLabel>
                  {isEditFlow ? (
                    <Flex flexDir={"column"} gap="1" alignItems={"end"}>
                      <DateAndTimePicker />
                      <Button
                        colorScheme="purple"
                        onClick={() =>
                          onUpdateDateTime(values?.startingDateTime)
                        }
                        isLoading={isSubmittingDateTime}
                      >
                        Update date and time
                      </Button>
                    </Flex>
                  ) : (
                    <DateAndTimePicker />
                  )}
                  <FormErrorMessage>
                    <ErrorMessage name="tickerPriceAmount" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.categoryId && touched.categoryId)}
                >
                  <FormLabel htmlFor="categoryId">Category:</FormLabel>
                  <Select
                    id="categoryId"
                    name="categoryId"
                    placeholder="Select category"
                    value={values.categoryId}
                    onChange={(e) =>
                      setFieldValue("categoryId", e.target.value)
                    }
                    disabled={isEditFlow}
                  >
                    {categories?.map((option) => (
                      <option key={option.id} value={option.id}>
                        {option.name}
                      </option>
                    ))}
                  </Select>
                  <FormErrorMessage>
                    <ErrorMessage name="categoryId" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.performerId && touched.performerId)}
                >
                  <FormLabel htmlFor="categoryId">Performer:</FormLabel>
                  <Select
                    id="performerId"
                    name="performerId"
                    placeholder="Select performer"
                    value={values.performerId}
                    onChange={(e) =>
                      setFieldValue("performerId", e.target.value)
                    }
                    disabled={isEditFlow}
                  >
                    {performers?.map((option) => (
                      <option key={option.id} value={option.id}>
                        {option.name}
                      </option>
                    ))}
                  </Select>
                  <FormErrorMessage>
                    <ErrorMessage name="eventType" />
                  </FormErrorMessage>
                </FormControl>
              </DrawerBody>

              <DrawerFooter>
                {!isEditFlow && (
                  <Button variant="outline" mr={3} onClick={onClose}>
                    Cancel
                  </Button>
                )}
                {!isEditFlow && (
                  <Button colorScheme="purple" type="submit">
                    Save
                  </Button>
                )}
              </DrawerFooter>
            </Form>
          )}
        </Formik>
      </DrawerContent>
    </Drawer>
  );
};

export default ShowModal;
