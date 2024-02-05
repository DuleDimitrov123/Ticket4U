import { useState } from "react";
import { Field, Form, Formik } from "formik";
import * as Yup from "yup";
import { Link, Navigate } from "react-router-dom";
import SignupLayout from "../../layout/SignupLayout/SignupLayout";
import {
  Alert,
  AlertIcon,
  Button,
  FormControl,
  FormErrorMessage,
  FormLabel,
  HStack,
  Input,
  Stack,
  Text,
  Grid,
  GridItem,
  Box,
} from "@chakra-ui/react";
import styles from "./Signup.styles";
import PublicLayout from "../../layout/PublicLayout/PublicLayout";
import useUser from "../../hooks/useUser";

const SignupSchema = Yup.object().shape({
  firstName: Yup.string().required("First name is required"),
  lastName: Yup.string().required("Last name is required"),
  email: Yup.string().email("Invalid email").required("Email is required"),
  username: Yup.string().required("Username is required"),
  password: Yup.string().required("Password is required"),
});

const Signup = () => {
  const [redirectOnLogin, setRedirectOnLogin] = useState(false);
  const { register } = useUser();
  const [signupSuccess, setSignupSuccess] = useState(false);
  const [signupError, setSignupError] = useState(false);

  const submitCredentials = async (credentials) => {
    try {
      await register.mutateAsync(credentials);
      setSignupSuccess("SignUp successful!");
      setSignupError(null);
      setTimeout(() => {
        setRedirectOnLogin(true);
      }, 700);
    } catch (error) {
      setSignupError(
        error.response?.data?.message || "An unknown error occurred"
      );
      setSignupSuccess(null);
    }
  };

  return (
    <PublicLayout>
      <Grid w="100%" h="100%" templateColumns="1fr 1fr" gap={0}>
        <GridItem
          bgImage="url(/images/Events.jpg)"
          bgSize="cover"
          bgPosition="center"
          bgRepeat="no-repeat"
          position="relative"
        >
          <Box
            position="absolute"
            top={0}
            left={0}
            right={0}
            bottom={0}
            bg="rgba(0, 0, 0, 0.6)" // You can adjust the opacity here
          ></Box>
        </GridItem>
        <GridItem bg="rgba(0, 0, 0, 0.6)">
          {redirectOnLogin && <Navigate to="/login" />}
          <SignupLayout
            title="Sign up"
            subtitle={
              <>
                Already have an account?
                <Text as={Link} to="/login" color={"purple.300"} ml={1}>
                  Log in now
                </Text>
              </>
            }
          >
            <Formik
              initialValues={{
                firstName: "",
                lastName: "",
                email: "",
                username: "",
                password: "",
              }}
              onSubmit={(values) => submitCredentials(values)}
              validationSchema={SignupSchema}
            >
              {({ handleSubmit, errors, touched }) => (
                <Form onSubmit={handleSubmit}>
                  {signupSuccess && (
                    <Alert status="success" mb={3}>
                      <AlertIcon />
                      {signupSuccess}
                    </Alert>
                  )}
                  {signupError && (
                    <Alert status="error" mb={3}>
                      <AlertIcon />
                      {signupError}
                    </Alert>
                  )}
                  <Stack spacing={3}>
                    <HStack spacing={3}>
                      <FormControl isInvalid={!!errors.email && touched.email}>
                        <FormLabel>First Name</FormLabel>
                        <Field
                          as={Input}
                          id="firstName"
                          type="text"
                          name="firstName"
                          placeholder="First Name"
                        />
                        {errors.firstName && touched.firstName ? (
                          <FormErrorMessage>
                            {errors.firstName}
                          </FormErrorMessage>
                        ) : null}
                      </FormControl>
                      <FormControl isInvalid={!!errors.email && touched.email}>
                        <FormLabel>Last Name</FormLabel>
                        <Field
                          as={Input}
                          id="lastName"
                          type="text"
                          name="lastName"
                          placeholder="Last Name"
                        />
                        {errors.lastName && touched.lastName ? (
                          <FormErrorMessage>{errors.lastName}</FormErrorMessage>
                        ) : null}
                      </FormControl>
                    </HStack>
                    <FormControl isInvalid={!!errors.email && touched.email}>
                      <FormLabel>Email address</FormLabel>
                      <Field
                        as={Input}
                        id="email"
                        type="email"
                        name="email"
                        placeholder="Email address"
                      />
                      {errors.email && touched.email ? (
                        <FormErrorMessage>{errors.email}</FormErrorMessage>
                      ) : null}
                    </FormControl>
                    <FormControl
                      isInvalid={!!errors.username && touched.username}
                    >
                      <FormLabel>Username</FormLabel>
                      <Field
                        as={Input}
                        id="username"
                        type="username"
                        name="username"
                        placeholder="Username"
                      />
                      {errors.username && touched.username ? (
                        <FormErrorMessage>{errors.username}</FormErrorMessage>
                      ) : null}
                    </FormControl>
                    <FormControl
                      isInvalid={!!errors.password && touched.password}
                    >
                      <FormLabel>Password</FormLabel>
                      <Field
                        as={Input}
                        id="password"
                        type="password"
                        name="password"
                        placeholder="Password"
                      />
                      {errors.password && touched.password ? (
                        <FormErrorMessage>{errors.password}</FormErrorMessage>
                      ) : null}
                    </FormControl>
                    <Stack>
                      <Button
                        {...styles.button}
                        type="submit"
                        // isLoading={registerIsLoading}
                      >
                        Sign Up
                      </Button>
                    </Stack>
                  </Stack>
                </Form>
              )}
            </Formik>
          </SignupLayout>
        </GridItem>
      </Grid>
    </PublicLayout>
  );
};

export default Signup;
