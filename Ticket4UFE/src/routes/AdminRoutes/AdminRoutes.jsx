import { Route } from "react-router-dom";
import Performers from "../../pages/Performers";
import Categories from "../../pages/Categories";

const AdminRoutes = () => {
  return [
    <Route key="performers" path="/performers" element={<Performers />} />,
    <Route key="categories" path="categories" element={<Categories />} />,
  ];
};

export default AdminRoutes;
