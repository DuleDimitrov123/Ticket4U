import { createContext, useContext } from "react";
import axios from "axios";
import { AuthContext } from "./AuthContext";
import PropTypes from "prop-types";

const FetchContext = createContext();
const { Provider } = FetchContext;

const FetchProvider = ({ children }) => {
  const auth = useContext(AuthContext);
  const apiUrl = import.meta.env.VITE_API_URL;

  const protectedFetch = axios.create({
    baseURL: apiUrl,
  });

  protectedFetch.interceptors.request.use(
    (config) => {
      if (auth.authState.token) {
        config.headers.Authorization = `Bearer ${auth.authState.token}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  protectedFetch.interceptors.response.use(
    (response) => {
      return response;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  return (
    <Provider
      value={{
        protectedFetch,
      }}
    >
      {children}
    </Provider>
  );
};
FetchProvider.propTypes = {
  children: PropTypes.node,
};
export { FetchContext, FetchProvider };
