import { useEffect } from "react";
import useCustomTypeSelector from "./hooks/useCustomTypeSelector";
import { useAppDispatch } from "./hooks/useCustomUsersType";
import { fetchAllProducts } from "./redux/reducers/productsReducer";
import Products from "./components/Product/Products";
import { Container, ThemeProvider } from "@mui/material";
import theme from "./styles/theme/mainTheme";
import NavBar from "./components/UI/NavBar";
import { Navigate, Route, Routes } from "react-router";
import Cart from "./components/Product/cart/Cart";
import Login from "./components/User/Auth/Login";
import SignUp from "./components/User/SignUp";
import { fetchAllUsers } from "./redux/reducers/usersReducer";
import UpdateUser from "./components/User/UpdateUser";
import UpdateProduct from "./components/Product/UpdateProduct";
import CreateProduct from "./components/Product/CreateProduct";
import Banner from "./components/UI/Banner/Banner";
import ProductDetail from "./components/Product/ProductDetail";
import PrivateRoutes from "./PrivateRoutes";
import Home from "./components/Home";
import Order from "./components/Order";
import Users from "./components/AdminDashboard/Users";
import Dashboarddd from "./components/AdminDashboard/Dashboard";
import Orders from "./components/AdminDashboard/Orders";

const App = () => {
  const users = useCustomTypeSelector((state) => state.usersReducer);
  const accessToken = useCustomTypeSelector(
    (state) => state.authReducer.accessToken
  );
  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(fetchAllProducts());
    dispatch(fetchAllUsers());
  }, []);
  return (
     <ThemeProvider theme={theme}>
      <Container maxWidth="xl" sx={{ background: "#fff" }}>
        <NavBar></NavBar>
        <Banner></Banner>
        <Routes>
          <Route path="/Home" element={<Home />}></Route>
          <Route path="*" element={<Navigate to="/Home" />} />
          <Route path="/Products" element={<Products />}></Route>
          <Route path="/Cart" element={<Cart />}></Route> 
          <Route path="/Auth" element={<Login />}></Route>
          <Route path="/SignUp" element={<SignUp/>}></Route>
          {accessToken ? (
          <Route path="/UpdateUser" element={<UpdateUser />} />
           ) : (
          <Route path="/UpdateUser" element={<Navigate to="/Profile"/>} />
           )}
          <Route element={<PrivateRoutes/>}>
          <Route path="/CreateProduct" element={<CreateProduct/>}></Route>
          <Route path="/UpdateProduct" element={<UpdateProduct/>}></Route>
          <Route path="/dashboard" element={<Dashboarddd/>}></Route> 
          <Route path="/users" element={<Users/>}></Route> 
          <Route path="/orders" element={<Orders/>}></Route> 
          </Route> 
          <Route path="/Profile" element={<Login />}></Route> 
          <Route path="/Order" element={<Order/>} />
          <Route path="/details/:productId" element={<ProductDetail/>}></Route>
          <Route path="/products/:searchName" element={<Products/>}></Route>
        </Routes>
        </Container>
    </ThemeProvider>
  );
};

export default App;
