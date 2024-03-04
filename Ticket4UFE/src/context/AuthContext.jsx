import { createContext, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";

const AuthContext = createContext();
const { Provider } = AuthContext;

const AuthProvider = ({ children }) => {
  const navigate = useNavigate();
  const startLocation = useLocation();

  const userInfoString = localStorage.getItem("userInfo");
  const userInfo = userInfoString ? JSON.parse(userInfoString) : null;
  const expiresAt = localStorage.getItem("expiresAt");
  const token = localStorage.getItem("token");

  const [authState, setAuthState] = useState({
    loading: false,
    startLocation,
    token: token ? token : null,
    expiresAt,
    userInfo: userInfo || {},
  });

  console.log("authState", authState);
  const setAuthInfo = ({ token }) => {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const decodedPayload = JSON.parse(atob(base64));
    localStorage.setItem("userInfo", JSON.stringify(decodedPayload));
    localStorage.setItem("token", token);

    setAuthState((old) => ({
      ...old,
      token,
      userInfo: decodedPayload,
    }));
  };

  const setToken = (token) => {
    setAuthState((old) => ({ ...old, token }));
  };

  const setLoading = (loading) => {
    setAuthState((old) => ({ ...old, loading }));
  };

  const setUserInfo = (callback) => {
    setAuthState((old) => ({ ...old, usename: callback(old.userInfo) }));
  };

  const logout = () => {
    localStorage.removeItem("userInfo");
    localStorage.removeItem("token");
    setAuthState((old) => ({
      ...old,
      token: null,
      userInfo: null,
    }));
    navigate("/login");
  };

  const isAuthenticated = () => {
    if (!authState.token) {
      return false;
    }
    return true;
  };

  const isAdmin = () => {
    return authState.userInfo.roles === "Admin";
  };

  return (
    <Provider
      value={{
        authState,
        setAuthState: (authInfo) => setAuthInfo(authInfo),
        setToken,
        setLoading,
        setUserInfo,
        logout,
        isAuthenticated,
        isAdmin,
      }}
    >
      {children}
    </Provider>
  );
};

export { AuthContext, AuthProvider };
