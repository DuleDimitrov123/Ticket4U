import { Suspense } from "react";
import "./App.css";
import { QueryClient, QueryClientProvider } from "react-query";
import { BrowserRouter as Router } from "react-router-dom";
import { ChakraProvider } from "@chakra-ui/react";
import Loading from "./components/Loading";
import AppRoutes from "./routes";

import theme from "./theme";
import { AuthProvider } from "./context/AuthContext";
import { FetchProvider } from "./context/FetchContext";

const queryClient = new QueryClient();

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Router>
        <ChakraProvider theme={theme}>
          <Suspense fallback={<Loading />}>
            <AuthProvider>
              <FetchProvider>
                <AppRoutes />
              </FetchProvider>
            </AuthProvider>
          </Suspense>
        </ChakraProvider>
      </Router>
    </QueryClientProvider>
  );
}

export default App;
