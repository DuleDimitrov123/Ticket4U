import { useContext } from 'react';
import { AuthContext } from '../../context/AuthContext';
import { Navigate, Outlet } from 'react-router-dom';

const AdminRoutesHandler = () => {
  const auth = useContext(AuthContext);
  return auth.isAuthenticated() && auth.isAdmin() ? (
    <Outlet />
  ) : (
    <Navigate to="/" />
  );
};

export default AdminRoutesHandler;
