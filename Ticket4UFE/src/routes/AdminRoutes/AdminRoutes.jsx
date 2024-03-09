import { Route } from "react-router-dom";
import Performers from "../../pages/Performers";

const AdminRoutes = () => {
  return [
    <Route key="performers" path="/performers" element={<Performers />} />,
  ];
};

export default AdminRoutes;
