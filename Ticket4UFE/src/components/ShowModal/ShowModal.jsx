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
} from "@chakra-ui/react";
import useReservation from "../../hooks/useReservation";
import { useNavigate } from "react-router";
import { useRef } from "react";
import ImageUpload from "../ImageUpload/ImageUpload";
import DateAndTimePicker from "../DateAndTimePicker/DateAndTimePicker";
import useShows from "../../hooks/useShows";

const ShowModal = ({ isOpen, onClose, isEditFlow }) => {
  const { createShow } = useShows();
  const btnRef = useRef();
  const initialValues = {
    name: "",
    description: "",
    picture: "",
    location: "",
    numOfPlaces: "",
    ticketPriceCurrency: "",
    tickerPriceAmount: "",
    startingDateTime: "",
  };
  const userInfo = localStorage.getItem("userInfo");
  const userInfoJSON = JSON.parse(userInfo);
  const navigate = useNavigate();

  const validationSchema = Yup.object().shape({
    name: Yup.string().required("This field is required"),
    description: Yup.string().required("This field is required"),
    location: Yup.string().required("This filed is required"),
    picture: Yup.string().required("This filed is required"),
    numOfPlaces: Yup.number()
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
    try {
      await createShow.mutateAsync(data, {
        onSuccess: (data) => {
          console.log("success");
        },
      });

      onClose();
    } catch (error) {
      console.error("Error creating reservation:", error);
    } finally {
      setSubmitting(false);
    }
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
        <DrawerHeader>Create show</DrawerHeader>
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
                  <Field as={Input} type="string" id="name" name="name" />
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
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="description" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.picture && touched.picture)}
                >
                  <FormLabel htmlFor="picture">Image:</FormLabel>
                  <ImageUpload />
                  <FormErrorMessage>
                    <ErrorMessage name="picture" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.location && touched.location)}
                >
                  <FormLabel htmlFor="location">Location:</FormLabel>
                  <Field
                    as={Input}
                    type="string"
                    id="location"
                    name="location"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="location" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(errors.location && touched.location)}
                >
                  <FormLabel htmlFor="numOfPlaces">Number of places:</FormLabel>
                  <Field
                    as={Input}
                    type="number"
                    id="numOfPlaces"
                    name="numOfPlaces"
                  />
                  <FormErrorMessage>
                    <ErrorMessage name="numOfPlaces" />
                  </FormErrorMessage>
                </FormControl>
                <FormControl
                  isInvalid={Boolean(
                    errors.ticketPriceCurrency && touched.ticketPriceCurrency
                  )}
                >
                  <FormLabel htmlFor="ticketPriceCurrency">Currency:</FormLabel>
                  {/* Use the Chakra UI Select component for the currency selection */}
                  <Select
                    id="ticketPriceCurrency"
                    name="ticketPriceCurrency"
                    placeholder="Select currency"
                    // Add the necessary Formik props to handle changes
                    value={values.ticketPriceCurrency}
                    onChange={(e) =>
                      setFieldValue("ticketPriceCurrency", e.target.value)
                    }
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
                  <Field
                    as={Input}
                    type="number"
                    id="tickerPriceAmount"
                    name="tickerPriceAmount"
                  />
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
                  <DateAndTimePicker />
                  <FormErrorMessage>
                    <ErrorMessage name="tickerPriceAmount" />
                  </FormErrorMessage>
                </FormControl>
              </DrawerBody>

              <DrawerFooter>
                <Button variant="outline" mr={3} onClick={onClose}>
                  Cancel
                </Button>
                <Button colorScheme="purple" type="submit">
                  Save
                </Button>
              </DrawerFooter>
            </Form>
          )}
        </Formik>
      </DrawerContent>
    </Drawer>
  );
};

export default ShowModal;
