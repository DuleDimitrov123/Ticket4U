import { Route, Routes } from "react-router-dom";
import AdminRoutes, { AdminRoutesHandler } from "./AdminRoutes";
import AuthenticatedRoutes, {
  AuthenticatedRoutesHandler,
} from "./AuthenticatedRoutes";
import PublicRoutes from "./PublicRoutes";
import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import Loading from "../components/Loading";

const AppRoutes = () => {
  const auth = useContext(AuthContext);
  return !auth?.authState.loading ? (
    <Routes>
      <Route element={<AdminRoutesHandler />}>{AdminRoutes()}</Route>

      <Route element={<AuthenticatedRoutesHandler />}>
        {AuthenticatedRoutes()}
      </Route>

      {PublicRoutes()}
    </Routes>
  ) : (
    <Loading />
  );
};

export default AppRoutes;
