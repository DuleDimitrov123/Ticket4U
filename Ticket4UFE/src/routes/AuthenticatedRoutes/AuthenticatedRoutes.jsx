import { Route } from "react-router-dom";
import Shows from "../../pages/Shows/Shows";
import Reservations from "../../pages/Reservations";
import ShowPreview from "../../pages/ShowPreview";
import Categories from "../../pages/Categories";

const AuthenticatedRoutes = () => {
  return [
    <Route key="shows" path="/shows" element={<Shows />} />,
    <Route
      key="reservations"
      path="/reservations"
      element={<Reservations />}
    />,
    <Route key="showPreview" path="/shows/:showId" element={<ShowPreview />} />,
    <Route 
      key="categories"
      path="categories"
      element={<Categories />}
    />
  ];
};

export default AuthenticatedRoutes;
