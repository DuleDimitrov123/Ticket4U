import { useState } from "react";
import { Field, Form, Formik } from "formik";
import * as Yup from "yup";
import { Navigate } from "react-router-dom";
import { Link } from "react-router-dom";
import {
  FormControl,
  FormLabel,
  Input,
  Stack,
  Button,
  Text,
  Alert,
  AlertIcon,
  FormErrorMessage,
  Grid,
  GridItem,
  Box,
} from "@chakra-ui/react";

import styles from "./Login.styles";
import SignupLayout from "../../layout/SignupLayout/SignupLayout";
import PublicLayout from "../../layout/PublicLayout/PublicLayout";
import useUser from "../../hooks/useUser";
// import useUser from '../../hooks/useUser';

const LoginSchema = Yup.object().shape({
  email: Yup.string().required("Email is required"),
  password: Yup.string().required("Password is required"),
});

const Login = () => {
  const { authenticate } = useUser();
  const [loginSuccess, setLoginSuccess] = useState();
  const [loginError, setLoginError] = useState();
  const [redirectOnLogin, setRedirectOnLogin] = useState(false);

  const submitCredentials = async (credentials) => {
    try {
      await authenticate.mutateAsync(credentials);
      setLoginSuccess("Login successful!");
      setLoginError(null);
      setTimeout(() => {
        setRedirectOnLogin(true);
      }, 700);
    } catch (error) {
      setLoginError(
        error.response?.data?.message || "An unknown error occurred"
      );
      setLoginSuccess(null);
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
        {redirectOnLogin && <Navigate to="/shows" />}
        <GridItem flex={1} bg="rgba(0, 0, 0, 0.6)">
          <SignupLayout
            title="Login"
            subtitle={
              <>
                Don't have an account?
                <Text as={Link} to="/signup" color={"purple.300"} ml={1}>
                  Sign up now
                </Text>
              </>
            }
          >
            <Formik
              initialValues={{
                email: "",
                password: "",
              }}
              onSubmit={(values) => submitCredentials(values)}
              validationSchema={LoginSchema}
            >
              {({ handleSubmit, errors, touched }) => (
                <Form onSubmit={handleSubmit}>
                  {loginSuccess && (
                    <Alert status="success" mb={3}>
                      <AlertIcon />
                      {loginSuccess}
                    </Alert>
                  )}
                  {loginError && (
                    <Alert status="error" mb={3}>
                      <AlertIcon />
                      {loginError}
                    </Alert>
                  )}
                  <Stack spacing={3}>
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
                        // isLoading={authenticationLoading}
                      >
                        Sign In
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
export default Login;
