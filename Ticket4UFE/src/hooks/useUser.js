import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import { publicFetch } from "../util/fetch";
import { useMutation } from "react-query";

const useUser = () => {
  const auth = useContext(AuthContext);
  const user = auth ? auth.authState.userInfo : { role: "" };

  const authenticateCallback = async ({ email, password }) => {
    const { data } = await publicFetch.post("/authenticate", {
      email,
      password,
    });
    return data;
  };

  const registerCallback = async ({
    firstName,
    lastName,
    email,
    username,
    password,
  }) => {
    const { data } = await publicFetch.post(`/register`, {
      firstName,
      lastName,
      email,
      username,
      password,
    });

    return data;
  };
  const authenticate = useMutation(authenticateCallback, {
    onSuccess: (data) => auth.setAuthState(data),
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
  });

  const register = useMutation(registerCallback, {
    onError: (error) => {
      return error.response?.data || "An unknown error occurred";
    },
  });

  return {
    user,
    register,
    registerIsLoading: register.isLoading,
    authenticate,
    authenticationLoading: authenticate.isLoading,
  };
};

export default useUser;
