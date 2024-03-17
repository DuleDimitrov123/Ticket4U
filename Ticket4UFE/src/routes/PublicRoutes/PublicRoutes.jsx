import { Route } from "react-router-dom";
import Login from "../../pages/Login";
import Signup from "../../pages/Signup";
import Home from "../../pages/Home/Home";
import NotFound from "../../pages/NotFound";
import Shows from "../../pages/Shows/Shows";

const PublicRoutes = () => {
  return [
    <Route key="login" path="/login" element={<Login />} />,
    <Route key="signup" path="/signup" element={<Signup />} />,
    <Route key="home" exact path="/" element={<Home />} />,
    <Route key="404" path="*" element={<NotFound />} />,
    <Route key="shows" path="/shows" element={<Shows />} />,
  ];
};

export default PublicRoutes;
