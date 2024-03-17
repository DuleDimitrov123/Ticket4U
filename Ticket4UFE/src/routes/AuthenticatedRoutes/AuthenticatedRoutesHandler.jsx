import { useContext } from "react";
import { AuthContext } from "../../context/AuthContext";
import { Navigate, Outlet } from "react-router-dom";

const AuthenticatedRoutesHandler = () => {
  const auth = useContext(AuthContext);
  return auth.isAuthenticated() ? <Outlet /> : <Navigate to="/" />;
};

export default AuthenticatedRoutesHandler;
